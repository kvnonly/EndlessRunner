using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [Space(15)]
    [SerializeField] private PlayerData _data;
    [SerializeField] private InputHandler _inputHandler;
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
        }
        else if (_inputHandler.LeftSide)
        {
            if (!_isMovingLeft && _currentLaneIndex > 0)
            {
                _currentLaneIndex--;
                _isMovingLeft = true;
                _isMovingRight = false;
            }
        }
        else
        {
            _isMovingRight = false;
            _isMovingLeft = false;
        }

        Vector3 targetPosition = _characterController.transform.position;

        // Calcula a nova posição X com base no índice da pista e na largura das ruas
        targetPosition.x = _currentLaneIndex * _laneWidth;

        // Calcula a posição alvo do personagem considerando o movimento para frente
        targetPosition += transform.forward * _data.ForwardSpeed * Time.deltaTime;

        // Move o personagem para a posição alvo utilizando o CharacterController
        _characterController.Move(targetPosition - _characterController.transform.position);
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

    #endregion
}
