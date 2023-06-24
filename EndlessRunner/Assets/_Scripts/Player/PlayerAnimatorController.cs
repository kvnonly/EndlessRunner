using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _player; // Referência ao script PlayerController
    private Animator _anim; // Referência ao componente Animator
    [SerializeField] PlayerData _data; // Dados do jogador (pode ser uma referência a um script de dados personalizado)
    [SerializeField] private InputHandler _input; // Componente responsável por lidar com entrada de input

    // Bools
    private bool _slidingFlag = false; // Flag que indica se o jogador está deslizando

    // Coroutine
    private Coroutine _resetSlideCoroutine; // Referência à Coroutine para redefinir a flag de deslize

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>(); // Obtém o componente PlayerController do objeto
        _anim = GetComponent<Animator>(); // Obtém o componente Animator do objeto
    }

    // Update is called once per frame
    void Update()
    {
        Slide(); // Verifica se o jogador deve deslizar
        StartCoroutine(Dead());
        UpdateValues(); // Atualiza os valores das variáveis no Animator
    }

    // Atualiza os valores das variáveis no Animator
    private void UpdateValues()
    {
        _anim.SetBool("IsSliding", _slidingFlag); // Define o valor da variável "IsSliding" no Animator
        _anim.SetFloat("AxisZ", _data.ForwardSpeed); // Define o valor da variável "AxisZ" no Animator com base na velocidade para frente do jogador
        _anim.SetBool("IsJumping", _player.IsJumping); // Define o valor da variável "IsJumping" no Animator com base no estado de pulo do jogador
        _anim.SetBool("IsGrounded", _player.IsGrounded); // Define o valor da variável "IsGrounded" no Animator com base no estado de contato com o chão do jogador
    }

    // Verifica se o jogador deve deslizar
    private void Slide()
    {
        // Verifica se o jogador está no chão, pressionou o botão de deslizar, a animação de deslize terminou e a flag de deslize é falsa
        if (_player.IsGrounded && _input.IsSliding && _player.IsSlidingAnimationFinished && !_slidingFlag)
        {
            _slidingFlag = true; // Define a flag de deslize como verdadeira
            _player.IsSlidingAnimationFinished = false; // Define a animação de deslize como não finalizada

            if (_resetSlideCoroutine != null)
                StopCoroutine(_resetSlideCoroutine); // Interrompe a Coroutine anterior, se existir

            _resetSlideCoroutine = StartCoroutine(ResetSlideFlag()); // Inicia a Coroutine para redefinir a flag de deslize após um tempo
        }
    }

    private IEnumerator Dead()
    {
        _anim.SetBool("Dead", _player.IsDead);

        yield return new WaitForSeconds(0.3f);

        _anim.SetBool("Dead", false);
    }

    // Coroutine para redefinir a flag de deslize
    private IEnumerator ResetSlideFlag()
    {
        yield return new WaitForSeconds(0.25f); // Aguarda 1 segundo

        _slidingFlag = false; // Define a flag de deslize como falsa
        _player.IsSlidingAnimationFinished = true; // Define a animação de deslize como finalizada
    }

    // Método chamado quando a animação de deslize termina
    public void OnSlidingAnimationFinished()
    {
        _player.IsSlidingAnimationFinished = true; // Define a animação de deslize como finalizada
    }
}
