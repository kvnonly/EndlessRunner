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
    [SerializeField] private GameObject _leftPlotParentObject; // Objeto-pai para as casas
    [SerializeField] private GameObject _rightPlotParentObject; // Objeto-pai para as casas
    [SerializeField] private float _safeZone = 100f; // Zona segura antes de destruir as casas ultrapassadas

    private float _lastZPos; // Posição Z da última casa gerada
    private GameObject _lastLeftPlot; // Referência para a última casa da esquerda gerada
    private GameObject _lastRightPlot; // Referência para a última casa da direita gerada

    private void Start()
    {
        // Posiciona a última Z com base na posição da primeira casa da esquerda
        _lastZPos = _firstLeftPlot.transform.position.z;

        // Define a primeira casa da direita como a última casa gerada
        _lastRightPlot = _firstRightPlot;

        for (int i = 0; i < _initAmount; i++)
        {
            SpawnPlot(); // Gera as casas iniciais
        }
    }

    private void Update()
    {
        // Verifica se a posição Z do personagem ultrapassou a zona segura
        if (transform.position.z > _lastZPos - _safeZone)
        {
            Debug.Log("O método no update ta funcionando");
            DestroyOldPlots(); // Destroi as casas ultrapassadas
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

        float zPos = _lastZPos + _plotSize; // Calcula a nova posição Z para a próxima casa

        // Instancia as casas da esquerda e direita como filhas do objeto-pai
        GameObject spawnedLeftPlot = Instantiate(leftPlot, new Vector3(_LeftXPos, 0, zPos), leftPlot.transform.rotation);
        spawnedLeftPlot.transform.parent = _leftPlotParentObject.transform;

        GameObject spawnedRightPlot = Instantiate(rightPlot, new Vector3(_RightXPos, 0, zPos), Quaternion.Euler(0, 180, 0));
        spawnedRightPlot.transform.parent = _rightPlotParentObject.transform;

        _lastZPos += _plotSize; // Atualiza a posição Z da última casa gerada
        _lastLeftPlot = leftPlot; // Armazena a referência para a última casa da esquerda gerada
        _lastRightPlot = rightPlot; // Armazena a referência para a última casa da direita gerada
    }

private void DestroyOldPlots()
{
    // Obtém a posição Z da última casa gerada
    float lastPlotZ = _lastZPos - _plotSize;

    Debug.Log("DestroyOldPlots: Last Plot Z: " + lastPlotZ);

    // Percorre todas as casas filhas do objeto-pai da esquerda
    foreach (Transform child in _leftPlotParentObject.transform)
    {
        Debug.Log("DestroyOldPlots: Child Position Z: " + child.position.z);

        // Verifica se a posição Z da casa é menor que a posição Z da última casa gerada menos a zona segura
        if (child.position.z < lastPlotZ - _safeZone)
        {
            Debug.Log("DestroyOldPlots: Destroying Left Plot");
            Destroy(child.gameObject); // Destrói a casa ultrapassada
        }
    }

    // Percorre todas as casas filhas do objeto-pai da direita
    foreach (Transform child in _rightPlotParentObject.transform)
    {
        Debug.Log("DestroyOldPlots: Child Position Z: " + child.position.z);

        // Verifica se a posição Z da casa é menor que a posição Z da última casa gerada menos a zona segura
        if (child.position.z < lastPlotZ - _safeZone)
        {
            Debug.Log("DestroyOldPlots: Destroying Right Plot");
            Destroy(child.gameObject); // Destrói a casa ultrapassada
        }
    }
}




}
