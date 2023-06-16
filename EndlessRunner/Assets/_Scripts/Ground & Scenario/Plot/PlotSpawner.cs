using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _plots; // Lista de casas disponíveis para gerar
    [SerializeField] private int _initAmount = 4; // Quantidade inicial de casas a serem geradas
    [SerializeField] private float _plotSize = 30f; // Tamanho de cada casa
    [SerializeField] private float _LeftXPos; // Posição X para a casa da esquerda
    [SerializeField] private float _RightXPos; // Posição X para a casa da direita
    [SerializeField] private GameObject _firstLeftPlot; // Primeira casa da esquerda
    [SerializeField] private GameObject _firstRightPlot; // Primeira casa da direita
    [SerializeField] private GameObject _parentObject; // Objeto-pai para as casas

    private GameObject _lastLeftPlot; // Referência para a última casa da esquerda gerada
    private GameObject _lastRightPlot; // Referência para a última casa da direita gerada

    private void Start()
    {
        // Instancia as primeiras casas da esquerda e direita
        Instantiate(_firstLeftPlot, new Vector3(_LeftXPos, 0, 0), _firstLeftPlot.transform.rotation);
        Instantiate(_firstRightPlot, new Vector3(_RightXPos, 0, 0), Quaternion.Euler(0, 180, 0));

        _lastLeftPlot = _firstLeftPlot; // Define a primeira casa da esquerda como a última casa gerada
        _lastRightPlot = _firstRightPlot; // Define a primeira casa da direita como a última casa gerada

        for (int i = 0; i < _initAmount; i++)
        {
            SpawnPlot(); // Gera as casas adicionais
        }
    }

    public void SpawnPlot()
    {
        GameObject leftPlot;
        GameObject rightPlot;

        do
        {
            // Seleciona aleatoriamente as casas da lista
            leftPlot = _plots[Random.Range(0, _plots.Count)];
            rightPlot = _plots[Random.Range(0, _plots.Count)];
        }
        while (leftPlot == _lastLeftPlot || leftPlot == _lastRightPlot || rightPlot == _lastLeftPlot || rightPlot == _lastRightPlot);
        // Repete o processo até que uma combinação não repetida seja encontrada

        float zPos = _lastLeftPlot.transform.position.z + _plotSize; // Calcula a nova posição Z para a próxima casa

        // Instancia as casas da esquerda e direita como filhas do objeto-pai
        GameObject spawnedLeftPlot = Instantiate(leftPlot, new Vector3(_LeftXPos, 0, zPos), leftPlot.transform.rotation);
        GameObject spawnedRightPlot = Instantiate(rightPlot, new Vector3(_RightXPos, 0, zPos), Quaternion.Euler(0, 180, 0));
        spawnedLeftPlot.transform.parent = _parentObject.transform;
        spawnedRightPlot.transform.parent = _parentObject.transform;

        _lastLeftPlot = spawnedLeftPlot; // Atualiza a referência para a última casa da esquerda gerada
        _lastRightPlot = spawnedRightPlot; // Atualiza a referência para a última casa da direita gerada
    }
}
