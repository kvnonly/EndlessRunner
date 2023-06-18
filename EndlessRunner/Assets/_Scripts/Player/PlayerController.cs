using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [Space(15)]
    [SerializeField] private PlayerData _data;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private SpawnManager _spawnManager;
    private CharacterController _characterController;
    
    [Header("Ground, Checks and Tags")]
    [Space(15)]
    [SerializeField] private float _minimumDistanceToFall = 1f;
    [SerializeField] private LayerMask _ignoreForGroundCheck;
    [SerializeField] private GameObject _leftFoot;
    [SerializeField] private Vector3 _leftFootOffset;
    [SerializeField] private Vector3 _centerOffset;
    [SerializeField] private GameObject _rightFoot;
    [SerializeField] private Vector3 _rightFootOffset;

    [Header("Roads and Repetition Parameters")]
    [Space(15)]
    [SerializeField] private float _laneWidth;
    [SerializeField] private int _numLanes;

    [Header("Slide Parameters")]
    [Space(15)]
    [SerializeField] private float _originalCharacterControllerCenter;
    [SerializeField] private float _originalCharacterControllerHeight;
    //Movement

    private float _velocityY; // Velocidade vertical do personagem

    private int _currentLaneIndex;

    // Flags
    private bool _isMovingRight = false;
    private bool _isMovingLeft = false;
    private bool _isGrounded; // Flag que indica se o personagem está no chão

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _currentLaneIndex = Mathf.FloorToInt(_numLanes / 2f);
    }

    private void Update()
    {
        Walk();
        //HandleGravity();
        //Jump();
        //Slide();
    }

    #region Player Action Methods

    private void Walk()
    {
        if (_inputHandler.RightSide)
        {
            if (!_isMovingRight && _currentLaneIndex < _numLanes - 1)
            {
                _currentLaneIndex++;
                _isMovingRight = true;
                _isMovingLeft = false;
            }
            else if (!_isMovingRight && _currentLaneIndex >= _numLanes - 1)
            {
                // O personagem está na extremidade direita e tentou se mover novamente para a direita
                TakeDamage();
            }
        }
        else if (_inputHandler.LeftSide)
        {
            if (!_isMovingLeft && _currentLaneIndex > 0)
            {
                _currentLaneIndex--;
                _isMovingLeft = true;
                _isMovingRight = false;
            }
            else if (!_isMovingLeft && _currentLaneIndex <= 0)
            {
                // O personagem está na extremidade esquerda e tentou se mover novamente para a esquerda
                TakeDamage();
            }
        }
        else
        {
            _isMovingRight = false;
            _isMovingLeft = false;
        }

        // Calcula a posição alvo do personagem considerando o movimento para frente
        Vector3 targetPosition = transform.position + transform.forward * _data.ForwardSpeed * Time.deltaTime;

        // Calcula a nova posição X com base no índice da pista e na largura das ruas
        float targetX = _currentLaneIndex * _laneWidth;
        targetPosition.x = Mathf.Lerp(transform.position.x, targetX, _data.LateralSpeed * Time.deltaTime);
        targetPosition.y = _velocityY;
         

        // Move o personagem para a posição alvo utilizando o CharacterController
        _characterController.Move(targetPosition - transform.position);
    }

    private void HandleGravity()
    {
        bool isCenterGrounded = Physics.Raycast(transform.position + _centerOffset, -Vector3.up, out RaycastHit centerHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isRightFootGrounded = Physics.Raycast(_rightFoot.transform.position + _rightFootOffset, -Vector3.up, out RaycastHit rightFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);
        bool isLeftFootGrounded = Physics.Raycast(_leftFoot.transform.position + _leftFootOffset, -Vector3.up, out RaycastHit leftFootHit, _minimumDistanceToFall, _ignoreForGroundCheck);

        bool isAnyFootGrounded = isCenterGrounded || isRightFootGrounded || isLeftFootGrounded;

        if (isAnyFootGrounded)
        {
            // O personagem está no chão
            _isGrounded = true;
            _velocityY = 0f; // Reiniciar a velocidade vertical
        }
        else
        {
            // O personagem está no ar
            _isGrounded = false;
            _velocityY += Physics.gravity.y * _data.GravityScale * _data.GravityMultiplier * Time.deltaTime; // Aplicar gravidade
        }
    }

    private void OnDrawGizmos()
    {
        // Exibir raios no Editor Unity
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + _centerOffset, -Vector3.up * _minimumDistanceToFall);
        Gizmos.DrawRay(_rightFoot.transform.position + _rightFootOffset, -Vector3.up * _minimumDistanceToFall);
        Gizmos.DrawRay(_leftFoot.transform.position + _leftFootOffset, -Vector3.up * _minimumDistanceToFall);
    }

    private void Jump()
    {
        if (_isGrounded && _inputHandler.IsJumping)
        {
            // O personagem está no chão e o botão de pulo foi pressionado
            _velocityY = _data.JumpForce; // Aplica a força do pulo à velocidade vertical
            _isGrounded = false; // O personagem não está mais no chão
        }
    }

    private void Slide()
{
    if (_isGrounded && _inputHandler.IsSliding)
    {

        // Exemplo: Reduzir o tamanho da colisão do personagem
        Vector3 newSize = _characterController.bounds.size;
        newSize.y *= 0.5f; // Reduzir a altura da colisão pela metade
        _characterController.center = new Vector3(_characterController.center.x, _characterController.center.y * 0.5f, _characterController.center.z);
        _characterController.height = newSize.y;


        // Aplicar a velocidade lateral ajustada
        Vector3 targetPosition = transform.position + transform.forward * _data.ForwardSpeed * Time.deltaTime;
        float targetX = _currentLaneIndex * _laneWidth;
        targetPosition.x = Mathf.Lerp(transform.position.x, targetX, _data.LateralSpeed * _data.SlideSpeedMultiplier * Time.deltaTime);
        _characterController.Move(targetPosition - transform.position);
    }
    else
    {
        // Restaurar o tamanho da colisão do personagem quando não estiver deslizando
        Vector3 originalSize = _characterController.bounds.size;
        originalSize.y = _originalCharacterControllerHeight;
        _characterController.center = new Vector3(_characterController.center.x, _originalCharacterControllerCenter, _characterController.center.z);
        _characterController.height = originalSize.y;
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("RoadCollision"))
        {
            //Debug.Log("Colisão com a tag correta ocorreu!");
            _spawnManager.SpawnTriggerEntered();
        
        }
        else
        {
            //Debug.Log("Colisão com uma tag diferente ocorreu: " + other.tag);
        }
    }

    #endregion
}
