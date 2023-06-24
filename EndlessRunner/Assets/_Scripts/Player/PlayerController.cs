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

    // Parâmetros de slide
    [Header("Slide Parameters")]
    [Space(15)]
    private bool _isSlidingAnimationFinished = true;
    private Vector3 _originalCharacterControllerCenter; // Valor original do centro do CharacterController
    private float _originalCharacterControllerHeight; // Valor original da altura do CharacterController

    // Movimento
    private float _velocityY; // Velocidade vertical do personagem

    // Flags
    private bool _isMovingSides = false; // Flag de movimento lateral
    private bool _isGrounded; // Flag que indica se o personagem está no chão
    private bool _isJumping = false;
    private bool _isDead;

    // Propriedades

    public bool IsSlidingAnimationFinished { get { return _isSlidingAnimationFinished; } set { _isSlidingAnimationFinished = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    private void Awake()
    {
        _isDead = false;
        _characterController = GetComponent<CharacterController>();
        _originalCharacterControllerCenter = _characterController.center;
        _originalCharacterControllerHeight = _characterController.height;
        _data.ForwardSpeed = 5;
    }

    private void Update()
    {
        if(!_isDead)
        {
            HandleGravity(); // Método para lidar com a gravidade
            Jump(); // Método para fazer o personagem pular
            _data.UpdatingSpeed(); // Aumenta a velocidade gradativamente
            WalkVerify(); // Método para mover o personagem para frente
        }
    }

    #region Player Action Methods

    // Configurações de colisão ao deslizar
    public void SlidingCollisionSettings()
    {
        // Reduz o tamanho da colisão do personagem para deslizar
        Vector3 newSize = _characterController.bounds.size;
        newSize.y *= _data.SlideCollisionHeight;
        _characterController.center = new Vector3(_originalCharacterControllerCenter.x, _originalCharacterControllerCenter.y * _data.SlideCollisionCenterMultiplier, _originalCharacterControllerCenter.z);
        _characterController.height = newSize.y;

        _isSlidingAnimationFinished = false;
    }

    // Restaura as configurações de colisão padrão
    public void DefaultCollisionSettings()
    {
        // Restaura o tamanho da colisão do personagem quando não estiver deslizando
        _characterController.center = _originalCharacterControllerCenter;
        _characterController.height = _originalCharacterControllerHeight;
    }

    private void WalkVerify()
    {
        if (_inputHandler.XWalkInput > 0 && transform.position.x < 8.5f && !_isMovingSides)
        {
            _isMovingSides = true;
            StartCoroutine(RightMove());
        }
        else if (_inputHandler.XWalkInput < 0 && transform.position.x > 1.5f && !_isMovingSides)
        {
            _isMovingSides = true;
            StartCoroutine(LeftMove());
        }

        ForwardMove();
    }
    
    private void ForwardMove()
    {
        Vector3 movement = Vector3.forward * _data.ForwardSpeed;
        movement.y = _velocityY;

        _characterController.Move(movement * Time.deltaTime);
    }
    IEnumerator RightMove() // coroutine que move o player para a direita
    {

        Vector3 target;
        target.y = _velocityY;
        target.x = transform.position.x + 3f;

        float difference = target.x - _characterController.transform.position.x;

        while (difference >= 0)
        {
            _characterController.Move(Vector3.right * Time.deltaTime * _data.LateralSpeed); // character controller move o personagem na direção do vector3.right
            difference = target.x - transform.position.x; // calcula a diferença de posição depois de mover o player

            //Debug.Log(difference);

            yield return null;
        }

        _isMovingSides = false;  // define isMovingSides como falsa para que seja possivel se mover para os lados novamente
    }

    IEnumerator LeftMove() // coroutine que move o player para a direita
    {

        Vector3 target;
        target.y = _velocityY;
        target.x = transform.position.x - 3f;

        float difference = target.x - _characterController.transform.position.x;

        while (difference <= 0)
        {
            _characterController.Move(Vector3.left * Time.deltaTime * _data.LateralSpeed); // character controller move o personagem na direção do vector3.right
            difference = target.x - transform.position.x; // calcula a diferença de posição depois de mover o player

            //Debug.Log(difference);

            yield return null;
        }

        _isMovingSides = false;  // define isMovingSides como falsa para que seja possivel se mover para os lados novamente
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
            _velocityY = _data.JumpForce; // Aplica a força do pulo à velocidade vertical
            _isGrounded = false; // O personagem não está mais no chão

            // Define _isMovingSides como false para impedir o movimento lateral durante o pulo
            _isMovingSides = false;
        }
        else if (_isJumping && _isGrounded)
        {
            _isJumping = false;
        }
    }

    public void Death()
    {
        _isDead = true;
        _inputHandler.OnDisable();
        StartCoroutine(ResetDeadFlag());
    }

    private IEnumerator ResetDeadFlag()
    {

        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        Destroy(this);
    }

    #endregion
}
