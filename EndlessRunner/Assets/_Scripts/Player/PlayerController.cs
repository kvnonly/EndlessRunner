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

    [Header("Ground and Repetition")]
    [Space(15)]
    [SerializeField] private float _laneWidth;
    [SerializeField] private int _numLanes;

    private int _currentLaneIndex;

    //Flags
    private bool _isMovingRight = false;
    private bool _isMovingLeft = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _currentLaneIndex = Mathf.FloorToInt(_numLanes / 2f);
    }

    private void Update()
    {
        Walk();
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

        // Move o personagem para a posição alvo utilizando o CharacterController
        _characterController.Move(targetPosition - transform.position);
    }


    private void Slide()
    {
        // Implemente a lógica para o personagem deslizar.
    }

    private void Jump()
    {
        // Implemente a lógica para o personagem pular.
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
         _spawnManager.SpawnTriggerEntered();
    }

    #endregion
}
