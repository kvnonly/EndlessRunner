using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SpawnRoad _roadSpawn;

    void Start()
    {
        _roadSpawn = GetComponent<SpawnRoad>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered()
    {
        _roadSpawn.MoveRoads();
    }
}
