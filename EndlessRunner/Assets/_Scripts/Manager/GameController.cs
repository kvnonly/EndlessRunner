using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject _player; // Referência ao objeto do jogador
    [SerializeField] private InputHandler _inputHandler; // Referência ao componente InputHandler para lidar com entrada do jogador
    [SerializeField] private TMP_Text _distance; // Referência ao componente de texto para mostrar a distância percorrida
    [SerializeField] private TMP_Text _coins; // Referência ao componente de texto para mostrar a quantidade de moedas coletadas
    [SerializeField] private GameObject _gameOver; // Referência ao objeto do painel de game over

    private int _playerCoins = 0; // Quantidade de moedas coletadas pelo jogador

    private void Awake()
    {
        _playerCoins = 0; // Inicializa a quantidade de moedas coletadas como zero
        _inputHandler.OnEnable(); // Habilita o componente InputHandler
    }

    void Start()
    {
        _player = GameObject.Find("Player"); // Encontra o objeto do jogador na cena e armazena a referência
    }

    void Update()
    {
        UpdatingDistanceText(); // Atualiza o texto da distância a cada quadro
    }

    private void UpdatingDistanceText()
    {
        int distance = Mathf.RoundToInt(_player.transform.position.z); // Calcula a distância arredondada com base na posição Z do jogador
        _distance.text = distance.ToString() + "m"; // Atualiza o texto da distância com a nova distância calculada
    }

    private void UpdatingCoinsText()
    {
        _coins.text = _playerCoins.ToString(); // Atualiza o texto das moedas com a quantidade de moedas coletadas pelo jogador
    }

    public void CoinsCollected()
    {
        _playerCoins++; // Incrementa a quantidade de moedas coletadas
        UpdatingCoinsText(); // Atualiza o texto das moedas
    }

    public void GameOver()
    {
        _gameOver.SetActive(true); // Ativa o painel de game over
    }
}
