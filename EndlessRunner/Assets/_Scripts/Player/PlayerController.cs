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
    

    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Player Action Methods

    private void Walk()
    {

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
