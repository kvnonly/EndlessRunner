using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Obstacles Spawn")]
    [Space(15)]
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private float _lastSpawnZ;
    [SerializeField] private int _spawnInterval;
    [SerializeField] private int _spawnAmount;

    [Header("Obstacles Positions")]
    [Space(15)]

    [SerializeField] private float _xPosition;
    [SerializeField] private float _yPosition;

    private void SpawnObstacles()
    {
        _lastSpawnZ += _spawnInterval;

        for(int i = 0; i < _spawnAmount; i++)
        {
            if(Random.Range(0,4) == 0)
            {
                GameObject obstacle = _obstacles[Random.Range(0, _obstacles.Count)];
                
                Instantiate(obstacle, new Vector3(_xPosition, _yPosition,_lastSpawnZ), obstacle.transform.rotation);
            }
        }
    
                /*if(Random.Range(0,2) == 1)
                {
                    ObstacleCollectableSpace space = obstacle.GetComponent<ObstaclesCollectableSpace>();
                    Instantiate(_coins, new Vector3(space.GetLane(), 0, _lastSpawnZ + 1.5f), _coins.transform.rotation);
                }
            }
            else
            {
                if(Random.Range(0,2) == 1)
                {
                    Instantiate(_coins, new Vector3(0, 0, _lastSpawnZ + 1,5f), _coins.transform.position);
                }
            }*/
    }
}
