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
    [SerializeField] private float _slideSpeed;


    [Header("Gravity Parameters")]
    [Space(15)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _gravityMultiplier;


    #region GET & SET Methods

    public float LateralSpeed {get { return _lateralSpeed; } set { _lateralSpeed = value; }}
    public float ForwardSpeed {get {return _forwardSpeed; } set {_forwardSpeed = value; }}
    



    

    #endregion
}
