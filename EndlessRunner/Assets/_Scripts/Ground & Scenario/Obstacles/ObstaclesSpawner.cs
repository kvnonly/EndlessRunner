using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Obstacles Spawn")]
    [Space(15)]
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private List<GameObject> _coinPrefabs;
    [SerializeField] private Transform _coinsParentObject;
    [SerializeField] private GameObject _obstacleParentObject;
    [SerializeField] private float _obstacleSpacing;
    [SerializeField] private float _lastSpawnZ;
    [SerializeField] private int _spawnInterval;
    [SerializeField] private int _spawnAmount;

    [Header("Obstacles Positions")]
    [Space(15)]
    [SerializeField] private float _xPosition;
    [SerializeField] private float _yPosition;

    [Header("Components")]
    [Space(15)]
    [SerializeField] private GameObject _player;
    private List<GameObject> _spawnedObstacles;
    private List<GameObject> _spawnedCoins;

    [Header("Destruction Parameters")]
    [Space(15)]
    [SerializeField] private float _safeZone;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _distanceToDestroy;
    [SerializeField] private float _distanceToDestroyCoins;

    private float _playerZPos;

    private void Start()
    {
        _spawnedObstacles = new List<GameObject>();
        _spawnedCoins = new List<GameObject>();

        SpawnObstacles();
    }

    private void Update()
    {
        _playerZPos = _player.transform.position.z;

        if (_playerZPos > _startPoint.position.z + _safeZone)
        {
            DestroyPreviousObstacles();
            DestroyPreviousCoins();
            SpawnObstacles();
        }
    }

    private void SpawnObstacles()
    {
        _lastSpawnZ += _spawnInterval;

        for (int i = 0; i < _spawnAmount; i++)
        {
            if (Random.Range(0, 4) == 0)
            {
                GameObject obstacle = _obstacles[Random.Range(0, _obstacles.Count)];
                Vector3 spawnPosition = new Vector3(_xPosition, _yPosition, _lastSpawnZ + (i * _obstacleSpacing));

                GameObject spawnedObstacle = Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);
                spawnedObstacle.transform.parent = _obstacleParentObject.transform;

                _spawnedObstacles.Add(spawnedObstacle);

                ObstacleCollectableSpace space = spawnedObstacle.GetComponent<ObstacleCollectableSpace>();
                if (space != null && Random.Range(0, 2) == 1)
                {
                    GameObject coinPrefab = _coinPrefabs[Random.Range(0, _coinPrefabs.Count)];

                    float coinLane = space.GetLane();
                    if (coinLane != -20f)
                    {
                        Vector3 coinPosition = new Vector3(coinLane, 0, spawnPosition.z + 1.5f);
                        GameObject coin = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
                        coin.transform.parent = _coinsParentObject;

                        _spawnedCoins.Add(coin);
                    }
                }
            }
        }
    }

    private void DestroyPreviousCoins()
    {
        for (int i = _spawnedCoins.Count - 1; i >= 0; i--)
        {
            GameObject coin = _spawnedCoins[i];
            float coinZPos = coin.transform.position.z;

            if (coinZPos + _distanceToDestroyCoins < _playerZPos)
            {
                _spawnedCoins.RemoveAt(i);
                Destroy(coin);
            }
        }
    }

    private void DestroyPreviousObstacles()
    {
        for (int i = _spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = _spawnedObstacles[i];
            float obstacleZPos = obstacle.transform.position.z;

            if (obstacleZPos + _distanceToDestroy < _playerZPos)
            {
                _spawnedObstacles.RemoveAt(i);
                Destroy(obstacle);
            }
        }
    }
}
