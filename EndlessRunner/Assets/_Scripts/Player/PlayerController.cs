using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Componentes
    [Header("Components")]
    [Space(15)]
    [SerializeField] private PlayerData _data; // Dados do jogador
    [SerializeField] private InputHandler _inputHandler; // Gerenciador de entrada
    private CharacterController _characterController; // Componente CharacterController usado para controlar o personagem

    // Verificação de solo e tags
    [Header("Ground, Checks and Tags")]
    [Space(15)]
    [SerializeField] private float _minimumDistanceToFall = 1f; // Distância mínima para detectar queda
    [SerializeField] private LayerMask _ignoreForGroundCheck; // LayerMask para ignorar na verificação de solo
    [SerializeField] private GameObject _leftFoot; // Pé esquerdo
    [SerializeField] private Vector3 _leftFootOffset; // Deslocamento do pé esquerdo
    [SerializeField] private Vector3 _centerOffset; // Deslocamento do centro do personagem
    [SerializeField] private GameObject _rightFoot; // Pé direito
    [SerializeField] private Vector3 _rightFootOffset; // Deslocamento do pé direito

    // Parâmetros de estradas e repetição
    [Header("Roads and Repetition Parameters")]
    [Space(15)]
    [SerializeField] private float _roadWidth; // Largura da estrada
    [SerializeField] private float _leftRoadCenter; // Posição central da estrada esquerda
    [SerializeField] private float _middleRoadCenter; // Posição central da estrada do meio
    [SerializeField] private float _rightRoadCenter; // Posição central da estrada direita
    [SerializeField] private int _numLanes; // Número de pistas

    // Parâmetros de slide
    [Header("Slide Parameters")]
    [Space(15)]
    private bool _isSlidingAnimationFinished = true;
    [SerializeField] private float _originalCharacterControllerCenter; // Valor original do centro do CharacterController
    [SerializeField] private float _originalCharacterControllerHeight; // Valor original da altura do CharacterController

    // Movimento
    private float _velocityY; // Velocidade vertical do personagem
    private Vector3 _targetPosition;
    private int _currentLaneIndex; // Índice da pista atual

    // Flags
    private bool _isMovingRight = false; // Flag de movimento para a direita
    private bool _isMovingLeft = false; // Flag de movimento para a esquerda
    private bool _isGrounded; // Flag que indica se o personagem está no chão
    private bool _isJumping = false;
    private bool _isMovingSides;


    //GET e SET Methods

    public bool IsSlidingAnimationFinished { get { return _isSlidingAnimationFinished; } set { _isSlidingAnimationFinished = value; }}
    public bool IsGrounded {get { return _isGrounded; } set { _isGrounded = value; }}
    public bool IsJumping {get { return _isJumping; } set { _isJumping = value; }}

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _currentLaneIndex = Mathf.FloorToInt(_numLanes / 2f); // Define a pista inicial como a do meio
        _data.ForwardSpeed = 5;
    }

    private void Update()
    {
        HandleGravity(); // Método para lidar com a gravidade
        Jump(); // Método para fazer o personagem pular
        _data.UpdatingSpeed(); //Aumenta a velocidade gradativamente
        WalkVerify(); // Método para mover o personagem para frente
    }

    #region Player Action Methods
    public void SlidingCollisionSettings()
    {
         // Reduz o tamanho da colisão do personagem para deslizar
        Vector3 newSize = _characterController.bounds.size;
        newSize.y *= _data.SlideCollisionHeight;
        _characterController.center = new Vector3(_characterController.center.x, _characterController.center.y * _data.SlideCollisionCenterMultiplier, _characterController.center.z);
        _characterController.height = newSize.y;

        _isSlidingAnimationFinished = false;
    }

    public void DefaultCollisionSettings()
    {
        // Restaura o tamanho da colisão do personagem quando não estiver deslizando
        Vector3 originalSize = _characterController.bounds.size;
        originalSize.y = _originalCharacterControllerHeight;
        _characterController.center = new Vector3(_characterController.center.x, _originalCharacterControllerCenter, _characterController.center.z);
        _characterController.height = originalSize.y;
    }

    private void WalkVerify()
    {
        
        if (_inputHandler.XWalkInput > 0 && transform.position.x < 8.5f && !_isMovingSides)
        {
            _isMovingSides = true;
            StartCoroutine(RightMove());
        }

        /*
        horizontalDirection menor que zero = tecla pressionada para esquerda
        transform.position.x maior que -5 = player não está na faixa da esquerda
        !_isMovingSides = não está se movendo para os lados
        */
        else if (_inputHandler.XWalkInput < 0 && transform.position.x > 1.5f && ! _isMovingSides)
        {
            _isMovingSides = true; // define _isMovingSides como true para que não seja possivel realizar outro movimento até que o atual termine
            StartCoroutine(LeftMove());
        }
    }
    IEnumerator LeftMove() // coroutine que move o player para esquerda
    {

        Vector3 target; // cria o vector3 local "target"
        target.x = transform.position.x - 8; // armazena o valor da posição X do player -8 como o target / valor "-8" aqui tem que ser substituído pela distância das tracks do seu jogo

        float difference = target.x - _characterController.transform.position.x; // cria a variavel "difference" sendo a diferença entre a posição do player e a posição alvo

        while (difference <= 0)
        {
            _characterController.Move(Vector3.left * Time.deltaTime * _data.LateralSpeed); // character controller move o personagem na direção do vector3.left
            difference = target.x - transform.position.x; // calcula a diferença de posição depois de mover o player
           
            //Debug.Log(difference);

            yield return null;
        }

        _isMovingSides = false; // define _isMovingSides como falsa para que seja possivel se mover para os lados novamente

    }
    
    IEnumerator RightMove() // coroutine que move o player para a direita
    {

        Vector3 target;
        target.x = transform.position.x + 8;

        float difference = target.x - _characterController.transform.position.x;

        while (difference >= 0)
        {
            _characterController.Move(Vector3.right * Time.deltaTime * _data.LateralSpeed); // character controller move o personagem na direção do vector3.right
            difference = target.x - transform.position.x; // calcula a diferença de posição depois de mover o player

            //Debug.Log(difference);

            yield return null;
        }

        _isMovingSides = false;  // define _isMovingSides como falsa para que seja possivel se mover para os lados novamente

    }

    private void HandleGravity()
    {
        // Verifica se algum dos pés ou o centro do personagem está no chão
        bool isCenterGrounded = Physics.Raycast(transform.position + _centerOffset, -Vector3.up, out RaycastHit centerHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isRightFootGrounded = Physics.Raycast(_rightFoot.transform.position + _rightFootOffset, -Vector3.up, out RaycastHit rightFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isLeftFootGrounded = Physics.Raycast(_leftFoot.transform.position + _leftFootOffset, -Vector3.up, out RaycastHit leftFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);

        bool isAnyFootGrounded = isCenterGrounded || isRightFootGrounded || isLeftFootGrounded;

        if (isAnyFootGrounded)
        {
            // O personagem está no chão
            _isGrounded = true;
            _velocityY = -1f; // Reiniciar a velocidade vertical
        }
        else
        {
            // O personagem está no ar
            _isGrounded = false;
            _velocityY += _data.GravityScale * _data.GravityMultiplier * Time.deltaTime; // Aplicar gravidade
        }
    }

    private void OnDrawGizmos()
    {
        // Exibe os raios no Editor Unity para verificar a detecção de solo
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + _centerOffset, -Vector3.up * _minimumDistanceToFall);
        Gizmos.DrawRay(_rightFoot.transform.position + _rightFootOffset, -Vector3.up * _minimumDistanceToFall);
        Gizmos.DrawRay(_leftFoot.transform.position + _leftFootOffset, -Vector3.up * _minimumDistanceToFall);
    }

   private void Jump()
    {
        if (!_isJumping && _isGrounded && _inputHandler.IsJumping)
        {
            // O personagem está no chão e o botão de pulo foi pressionado
            _isJumping = true;
            _velocityY += _data.JumpForce; // Aplica a força do pulo à velocidade vertical
            _isGrounded = false; // O personagem não está mais no chão
        }
        else if(_isJumping && _isGrounded)
        {
             _isJumping = false;
        }
    }
    private void TakeDamage()
    {
        // Implemente a lógica para o personagem sofrer dano.
    }

    private void Death()
    {
        // Implemente a lógica para a morte do personagem.
    }

    #endregion
}
