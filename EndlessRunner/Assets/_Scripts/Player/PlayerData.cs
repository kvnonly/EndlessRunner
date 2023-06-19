using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Geral Status Parameters")]
    [Space(15)]

    [SerializeField] private int _life;
    [SerializeField] private int _coins;

    [Header("Movement Status Parameters")]
    [Space(15)]
    [SerializeField] private float _maxForwardSpeed; // Velocidade máxima do jogador
    [SerializeField] private float _forwardSpeedIncreaseRate; // Taxa de aumento da velocidade
    private float _currentForwardSpeed;

    [SerializeField] private float _lateralSpeed;
    [SerializeField] private float _forwardSpeed;

    [Header("Jump Parameters")]
    [Space(15)]
    [SerializeField] private float _jumpForce;

    [Header("Slide Parameters")]
    [Space(15)]
    [SerializeField] private float _slideSpeedMultiplier;
    [SerializeField] private float _slideCollisionHeight = 0.5f;
    [SerializeField] private float _slideCollisionCenterMultiplier = 0.5f;


    [Header("Gravity Parameters")]
    [Space(15)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _gravityMultiplier;


    #region GET & SET Methods

    public float LateralSpeed {get { return _lateralSpeed; } set { _lateralSpeed = value; }}
    public float ForwardSpeed {get {return _currentForwardSpeed; } set {_currentForwardSpeed = value; }}
    public float ForwardSpeedIncreaseRate { get { return _forwardSpeedIncreaseRate;} set { _forwardSpeedIncreaseRate = value; }}
    public float MaxForwardSpeed { get { return _maxForwardSpeed; } set { _maxForwardSpeed = value; }}

    public float GravityScale {get { return _gravityScale; } set { _gravityScale = value; }}
    public float GravityMultiplier {get { return _gravityMultiplier; } set { _gravityMultiplier = value; }}

    public float JumpForce {get { return _jumpForce; } set { _jumpForce = value; }}
    

    public float SlideSpeedMultiplier {get { return _slideSpeedMultiplier; } set { _slideSpeedMultiplier = value; }}
    public float SlideCollisionHeight {get { return _slideCollisionHeight;} set { _slideCollisionHeight = value; }}
    public float SlideCollisionCenterMultiplier {get { return _slideCollisionCenterMultiplier; } set { _slideCollisionCenterMultiplier = value; }}

    private void Awake()
    {
        _currentForwardSpeed = _forwardSpeed; // Define a velocidade inicial
    }
    public void UpdatingSpeed()
    {
        // Atualiza a velocidade atual com base na taxa de aumento
        _currentForwardSpeed += _forwardSpeedIncreaseRate * Time.deltaTime;

        // Limita a velocidade máxima
        _currentForwardSpeed = Mathf.Clamp(_currentForwardSpeed, _forwardSpeed, _maxForwardSpeed);
    }
    



    

    #endregion
}
