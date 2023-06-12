using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components & Other Scripts")]
    [Space(15)]
    [SerializeField] private PlayerData _data;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerAnimatorController _animator;
    private CharacterController _characterController;

    [Header("Layers, Checks & Tags")]
    [Space(15)]

    [SerializeField] private float _groundeDetectionRayStartPoint = 0.5f;
    [SerializeField] private float _minimumDistanceToFall = 1f;
    [SerializeField] private float _groundDirectionRayDistance = 0.2f;
    [SerializeField] private  LayerMask _groundLayer;

    [Header("Scenario & Array")]
    [Space(15)]

    [SerializeField] private Transform[] _lanes;
    private int _currentLaneIndex;

    

    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
    }


    #region Player Action Methods

    private void HandleMoveInput()
    {
        float moveX = _inputHandler.XWalkInput * _data.Speed * Time.deltaTime;
        float moveY = 0;
        float moveZ = transform.forward.z * _data.ForwardSpeed * Time.deltaTime;
        Vector3 direction = new Vector3(moveX, moveY , moveZ);

        _characterController.Move(direction);   
    }

    private void Slide()
    {

    }
    private void Jump()
    {

    }

    private void TakeDamage()
    {

    }
    private void Death()
    {

    }
    #endregion
}
