using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _player;
    private Animator _anim;
    [SerializeField] PlayerData _data;
    [SerializeField] private InputHandler _input; 

    //Bools

    private bool _slidingFlag = false;

    // Start is called before the first frame update

    void Start()
    {
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Slide();
        UpdateValues();
    }

    private void UpdateValues()
    {
        _anim.SetBool("IsSliding", _slidingFlag);
        _anim.SetFloat("AxisZ", _data.ForwardSpeed);
        _anim.SetBool("IsJumping", _player.IsJumping);
        _anim.SetBool("IsGrounded", _player.IsGrounded);
    }

    private void Slide()
    {
        if(_player.IsGrounded && _input.IsSliding && _player.IsSlidingAnimationFinished && !_slidingFlag)
        {
            _slidingFlag = true;
            _player.IsSlidingAnimationFinished = false;
        } 
    }
    

    public void OnSlidingAnimationFinished()
    {
        _player.IsSlidingAnimationFinished = true;
        _slidingFlag = false;
    }

}
