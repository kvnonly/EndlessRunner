using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Guardando as classes manager dentro de uma variavel para que
    //seja possivel acessa-las atraves da classe GameManager
    public static GameManager Instance;

    [Header("Managers")]
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Já existe uma instacia dessa classe!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }    
        //metodo para que o metodo game manager não seja destroido.
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //acesando a classe scene load manager para carregar o metodo loadScene
        _sceneLoadManager.LoadScene("Menu");
    }


    //propiedade/referencia publica  que faz com que seja possivel acessar o scene load manager para conseguir fazer atroca de cena e o audio manager.
    public SceneLoadManager SceneLoadManager => _sceneLoadManager;
    public AudioManager AudioManager => _audioManager;

    // Update is called once per frame
    void Update()
    {
        
    }
}
