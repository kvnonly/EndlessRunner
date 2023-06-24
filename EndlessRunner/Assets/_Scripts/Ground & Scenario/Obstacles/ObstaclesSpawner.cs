using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Obstacles Spawn")]
    [SerializeField] private List<GameObject> _obstacles; // Lista de obstáculos disponíveis para spawnar
    [SerializeField] private List<GameObject> _coinPrefabs; // Lista de prefabs de moedas disponíveis
    [SerializeField] private Transform _coinsParentObject; // Objeto pai das moedas instanciadas
    [SerializeField] private GameObject _obstacleParentObject; // Objeto pai dos obstáculos instanciados
    [SerializeField] private float _obstacleSpacing; // Espaçamento entre os obstáculos
    [SerializeField] private float _lastSpawnZ; // Posição Z do último obstáculo spawnado
    [SerializeField] private float _spawnDistance; // Distância entre cada spawn de obstáculo
    [SerializeField] private float _spawnInterval; // Intervalo de tempo entre cada spawn de obstáculo
    [SerializeField] private int _initialObstacleCount; // Quantidade inicial de obstáculos
    [SerializeField] private int _additionalObstacleCount; // Quantidade de obstáculos adicionais a serem instanciados durante o jogo

    [Header("Obstacles Positions")]
    [SerializeField] private float _xPosition; // Posição X dos obstáculos
    [SerializeField] private float _yPosition; // Posição Y dos obstáculos

    [Header("Components")]
    [SerializeField] private GameObject _player; // Referência ao objeto do jogador
    private List<GameObject> _spawnedObstacles; // Lista dos obstáculos spawnados
    private List<GameObject> _spawnedCoins; // Lista das moedas spawnadas

    private float _playerZPos; // Posição Z do jogador
    private float _nextSpawnTime; // Próximo momento para spawnar um obstáculo
    private int _obstacleCount; // Contador de obstáculos já instanciados durante o jogo

    private const float DestroyOffset = 10f; // Distância adicional para destruir os objetos

    private void Start()
    {
        _spawnedObstacles = new List<GameObject>(); // Inicializa a lista de obstáculos spawnados
        _spawnedCoins = new List<GameObject>(); // Inicializa a lista de moedas spawnadas
        _nextSpawnTime = _spawnInterval; // Define o próximo momento para spawnar um obstáculo

        SpawnInitialObstacles(); // Realiza o spawn dos obstáculos iniciais
    }

    private void Update()
    {
        _playerZPos = _player.transform.position.z; // Atualiza a posição Z do jogador

        // Verifica se ainda é necessário spawnar obstáculos e se já é o momento para isso
        if (_obstacleCount < _initialObstacleCount + _additionalObstacleCount && Time.time >= _nextSpawnTime)
        {
            SpawnObstacle(); // Realiza o spawn de um obstáculo
            _nextSpawnTime += _spawnInterval; // Atualiza o próximo momento para spawnar um obstáculo
        }

        DestroyPreviousObstacles(); // Destroi os obstáculos anteriores
        DestroyPreviousCoins(); // Destroi as moedas anteriores
    }

    private void SpawnInitialObstacles()
    {
        for (int i = 0; i < _initialObstacleCount; i++)
        {
            SpawnObstacle(); // Realiza o spawn de um obstáculo
        }
    }

    private void SpawnObstacle()
    {
        _lastSpawnZ += _obstacleSpacing; // Atualiza a posição Z do último obstáculo spawnado

        GameObject obstacle = _obstacles[Random.Range(0, _obstacles.Count)]; // Seleciona aleatoriamente um obstáculo da lista
        Vector3 spawnPosition = new Vector3(_xPosition, _yPosition, _lastSpawnZ); // Calcula a posição de spawn do obstáculo

        GameObject spawnedObstacle = Instantiate(obstacle, spawnPosition, obstacle.transform.rotation); // Instancia o obstáculo na posição de spawn
        spawnedObstacle.transform.parent = _obstacleParentObject.transform; // Define o objeto pai do obstáculo

        _spawnedObstacles.Add(spawnedObstacle); // Adiciona o obstáculo à lista de obstáculos spawnados
        _obstacleCount++; // Incrementa o contador de obstáculos

        ObstacleCollectableSpace space = spawnedObstacle.GetComponent<ObstacleCollectableSpace>(); // Obtém o componente ObstacleCollectableSpace do obstáculo
        if (space != null && Random.Range(0, 2) == 1) // Verifica se o obstáculo possui um espaço para coletáveis e se um número aleatório é igual a 1
        {
            GameObject coinPrefab = _coinPrefabs[Random.Range(0, _coinPrefabs.Count)]; // Seleciona aleatoriamente um prefab de moeda da lista

            float coinLane = space.GetLane(); // Obtém a posição da pista onde a moeda será spawnada a partir do componente ObstacleCollectableSpace
            if (coinLane != -20f) // Verifica se a moeda não será spawnada fora da pista (-20f é um valor de marcação para ausência de pista)
            {
                Vector3 coinPosition = new Vector3(coinLane, 0, spawnPosition.z + 1.5f); // Calcula a posição de spawn da moeda
                GameObject coin = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation); // Instancia a moeda na posição de spawn
                coin.transform.parent = _coinsParentObject; // Define o objeto pai da moeda

                _spawnedCoins.Add(coin); // Adiciona a moeda à lista de moedas spawnadas
            }
        }
    }

    private void DestroyPreviousCoins()
    {
        for (int i = _spawnedCoins.Count - 1; i >= 0; i--)
        {
            GameObject coin = _spawnedCoins[i]; // Obtém a moeda da lista
            float coinZPos = coin.transform.position.z; // Obtém a posição Z da moeda

            if (coinZPos + DestroyOffset < _playerZPos) // Verifica se a moeda está atrás do jogador além da distância de destruição
            {
                _spawnedCoins.RemoveAt(i); // Remove a moeda da lista
                Destroy(coin); // Destroi a moeda
            }
        }
    }

    private void DestroyPreviousObstacles()
    {
        for (int i = _spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = _spawnedObstacles[i]; // Obtém o obstáculo da lista
            float obstacleZPos = obstacle.transform.position.z; // Obtém a posição Z do obstáculo

            if (obstacleZPos + DestroyOffset < _playerZPos) // Verifica se o obstáculo está atrás do jogador além da distância de destruição
            {
                _spawnedObstacles.RemoveAt(i); // Remove o obstáculo da lista
                Destroy(obstacle); // Destroi o obstáculo
            }
        }
    }
}
