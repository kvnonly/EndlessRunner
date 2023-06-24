using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private GameControls _gameControls;

    private Vector2 _walkInput;
    private bool _jumpPressed;
    private bool _slidePressed;
    private bool _usePressed;
    private bool _leftSide;
    private bool _rightSide;
    //Para o acesso das variaveis de movimento e ações do jogador, métodos get e set são criados para que o acesso de outros scripts seja possível da mesma forma que a torna mais dificil de ser editada por fora
    // Os métodos abaixo serão usados em outros scripts em vez de acessar diretamente as variaveis
    public Vector2 WalkInput {get {return _walkInput; } set { _walkInput = value;}}
    public float XWalkInput {get { return _walkInput.x; } set { _walkInput.x = value; }}
    public bool IsJumping {get { return _jumpPressed; } set { _jumpPressed = value ;}}
    public bool IsSliding {get { return _slidePressed; } set { _slidePressed = value; }}
    public bool IsUsePressed {get { return _usePressed;} set { _usePressed = value; }}
    public bool LeftSide {get { return _leftSide;} set { _leftSide = value; }}
    public bool RightSide {get { return _rightSide;} set { _rightSide = value; }}
    
    private void Awake() 
    {

        #region Input Data
        _gameControls = new GameControls();

        //As linhas abaixo estão recebendo quando o botão é clicado ou não e passando o valor do mesmo para os métodos 

        _gameControls.PlayerMovement.Walk.performed += OnWalk;
        _gameControls.PlayerMovement.Walk.canceled += OnWalk;

        _gameControls.PlayerAction.Jump.performed += OnJump;
        _gameControls.PlayerAction.Jump.canceled += OnJump;

        _gameControls.PlayerAction.Slide.performed += OnSlide;
        _gameControls.PlayerAction.Slide.canceled += OnSlide;

        _gameControls.PlayerAction.Using.performed += OnUse;
        _gameControls.PlayerAction.Using.canceled += OnUse;

        #endregion
        
    }

    #region Player Input Methods
    public void OnWalk(InputAction.CallbackContext ctx)
    {
        //Quando a ação de pressionar ou clicar no botão será definido abaixo um valor para a varivavel criada
        _walkInput = ctx.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        //Quando a ação de pressionar ou clicar no botão será definido abaixo um valor para a varivavel criada, no caso de ser uma booleana receberá true ou false
        _jumpPressed = ctx.ReadValueAsButton();
    }
    public void OnSlide(InputAction.CallbackContext ctx)
    {
        //Quando a ação de pressionar ou clicar no botão será definido abaixo um valor para a varivavel criada, no caso de ser uma booleana receberá true ou false
        _slidePressed = ctx.ReadValueAsButton();
    }

     public void OnUse(InputAction.CallbackContext ctx)
    {
        //Quando a ação de pressionar ou clicar no botão será definido abaixo um valor para a varivavel criada, no caso de ser uma booleana receberá true ou false
        _usePressed = ctx.ReadValueAsButton();
    }

    #endregion


    #region Input Management


    public void OnEnable() 
    {
        _gameControls.Enable();    
    }
    public void OnDisable() 
    {
        _gameControls.Disable();   
    }

    #endregion    
}
