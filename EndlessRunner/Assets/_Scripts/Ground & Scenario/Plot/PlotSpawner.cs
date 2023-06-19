using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{

    [Header("Components")]
    [Space(15)]

    [SerializeField] private List<GameObject> _plots;
    [SerializeField] private GameObject _player;
    private List<GameObject> _spawnedPlots;

    [Header("Destruction Parameters")]
    [Space(15)]
    [SerializeField] private float _safeZone;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _distanceToDestroy;

    [Header("House Parameters")]
    [Space(15)]
    [SerializeField] private GameObject _leftPlotParentObject;
    [SerializeField] private GameObject _rightPlotParentObject;
    [SerializeField] private GameObject _firstLeftHouse;
    [SerializeField] private GameObject _firstRightHouse;
    [SerializeField] private int _initAmount = 4;
    [SerializeField] private float _plotSize;
    [SerializeField] private float _LeftXPos;
    [SerializeField] private float _RightXPos;


    //Player Parameters
    private float _playerZPos;

    
    private void Start()
    {
        _spawnedPlots = new List<GameObject>();

        for (int i = 0; i < _initAmount; i++)
        {
            SpawnPlot();
        }
    }

    private void Update()
    {
        _playerZPos = _player.transform.position.z;

        if (_playerZPos > _startPoint.position.z + _safeZone)
        {
            DestroyPreviousPlots();
        }
    }

    public void SpawnPlot()
    {
        GameObject leftPlot;
        GameObject rightPlot;

        do
        {
            leftPlot = _plots[Random.Range(0, _plots.Count)]; // Seleciona aleatoriamente uma casa à esquerda da lista de casas disponíveis
            rightPlot = _plots[Random.Range(0, _plots.Count)]; // Seleciona aleatoriamente uma casa à direita da lista de casas disponíveis
        }
        while (leftPlot == rightPlot); // Repete o processo se ambas as casas forem iguais

        float zPos = (_spawnedPlots.Count > 0) ? _spawnedPlots[_spawnedPlots.Count - 1].transform.position.z + _plotSize : 0f; // Determina a posição Z da nova casa com base na posição Z da última casa instanciada

        GameObject spawnedLeftPlot = Instantiate(leftPlot, new Vector3(_LeftXPos, 0, zPos), leftPlot.transform.rotation, _leftPlotParentObject.transform); // Instancia a casa à esquerda com a posição e rotação corretas
        GameObject spawnedRightPlot = Instantiate(rightPlot, new Vector3(_RightXPos, 0, zPos), Quaternion.Euler(-90, 180, 0), _rightPlotParentObject.transform); // Instancia a casa à direita com a posição e rotação corretas
        
        _spawnedPlots.Add(spawnedLeftPlot); // Adiciona a casa à esquerda à lista de casas instanciadas
        _spawnedPlots.Add(spawnedRightPlot); // Adiciona a casa à direita à lista de casas instanciadas
    }

    private void DestroyPreviousPlots()
    {
        for (int i = _spawnedPlots.Count - 1; i >= 0; i--)
        {
            GameObject plot = _spawnedPlots[i]; // Obtém uma referência para a casa atual da iteração

            float plotZPos = plot.transform.position.z; // Obtém a posição Z da casa atual

            // Verificar se as casas instanciadas ou a casa inicial está além da distância de destruição
            if (plotZPos + _distanceToDestroy < _playerZPos) // Verifica se a casa está além da distância de destruição
            {
                _spawnedPlots.Remove(plot); // Remove a casa da lista de casas instanciadas
                Destroy(_firstLeftHouse); // Destroi a casa inicial à esquerda
                Destroy(_firstRightHouse); // Destroi a casa inicial à direita
                Destroy(plot); // Destroi a casa atual
                //Debug.Log("Destroyed Plot: " + plot.transform.position); // Exibe um log informando a destruição da casa
            }
        }
    }

}