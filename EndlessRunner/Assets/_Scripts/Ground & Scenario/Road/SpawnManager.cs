using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SpawnRoad _roadSpawn;                   // Referência ao script responsável pelo spawn de estradas
    private PlotSpawner _plotSpawn;                 // Referência ao script responsável pelo spawn de terrenos
    private ObstaclesSpawner _obstacleSpawn;        // Referência ao script responsável pelo spawn de obstáculos
    [SerializeField] private int _totalAmount = 16; // Número total de elementos a serem spawnados

    void Start()
    {
        _roadSpawn = GetComponent<SpawnRoad>();     // Obtém o componente SpawnRoad do GameObject atual e armazena a referência
        _plotSpawn = GetComponent<PlotSpawner>();   // Obtém o componente PlotSpawner do GameObject atual e armazena a referência
        _obstacleSpawn = GetComponent<ObstaclesSpawner>();  // Obtém o componente ObstaclesSpawner do GameObject atual e armazena a referência
    }

    // Método chamado quando ocorre o acionamento do evento de spawn
    public void SpawnTriggerEntered()
    {
        _roadSpawn.MoveRoads();                     // Chama o método MoveRoads do script SpawnRoad para mover as estradas

        for (int i = 0; i < _totalAmount; i++)
        {
            _plotSpawn.SpawnPlot();                 // Chama o método SpawnPlot do script PlotSpawner para spawnar terrenos
        }
    }
}
