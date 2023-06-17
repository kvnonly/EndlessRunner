using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Parameters")]
    [Space(15)]
    [SerializeField] private float _lateralSpeed;
    [SerializeField] private float _forwardSpeed;

    [Header("Jump Parameters")]
    [Space(15)]
    [SerializeField] private float _jumpForce;

    [Header("Slide Parameters")]
    [Space(15)]
    [SerializeField] private float _slideSpeedMultiplier;


    [Header("Gravity Parameters")]
    [Space(15)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _gravityMultiplier;


    #region GET & SET Methods

    public float LateralSpeed {get { return _lateralSpeed; } set { _lateralSpeed = value; }}
    public float ForwardSpeed {get {return _forwardSpeed; } set {_forwardSpeed = value; }}

    public float GravityScale {get { return _gravityScale; } set { _gravityScale = value; }}
    public float GravityMultiplier {get { return _gravityMultiplier; } set { _gravityMultiplier = value; }}

    public float JumpForce {get { return _jumpForce; } set { _jumpForce = value; }}
    

    public float SlideSpeedMultiplier {get { return _slideSpeedMultiplier; } set { _slideSpeedMultiplier = value; }}
    



    

    #endregion
}
