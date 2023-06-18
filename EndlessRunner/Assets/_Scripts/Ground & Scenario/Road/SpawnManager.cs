using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SpawnRoad _roadSpawn;
    private PlotSpawner _plotSpawn;
    [SerializeField] private int _totalAmount = 16;

    void Start()
    {
        _roadSpawn = GetComponent<SpawnRoad>();
        _plotSpawn = GetComponent<PlotSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered()
    {
        _roadSpawn.MoveRoads();

        for (int i = 0; i < _totalAmount; i++)
        {
            _plotSpawn.SpawnPlot();
        }
    }
}
