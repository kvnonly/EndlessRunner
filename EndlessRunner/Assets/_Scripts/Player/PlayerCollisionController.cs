using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager; // Gerenciador de spawn
    [SerializeField] private GameController _gameController;
    private CharacterController _characterController;

    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("RoadCollision"))
    {   
        _spawnManager.SpawnTriggerEntered();
    }

    if (other.CompareTag("Note 1") || other.CompareTag("Note 2") || other.CompareTag("Note 3"))
    {
        Destroy(other.gameObject);
        _gameController.CoinsCollected();
    }
}   
}
