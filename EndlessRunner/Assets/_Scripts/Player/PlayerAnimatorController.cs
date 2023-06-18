using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private InputHandler _input; 
    // Start is called before the first frame update

    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnSlidingAnimationFinished()
    {
        _player.IsSlidingAnimationFinished = true;
    }
}
