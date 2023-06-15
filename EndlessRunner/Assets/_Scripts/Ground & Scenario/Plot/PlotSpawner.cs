using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [SerializeField] private int _initAmount = 5; // Quantidade inicial de casas a serem geradas
    [SerializeField] private float _initStartZ = 61f; // Posição inicial em Z para a primeira casa
    [SerializeField] private float _plotSize = 6f; // Tamanho de cada casa
    [SerializeField] private float _xPosLeft = -18.61f; // Posição em X para a casa da esquerda
    [SerializeField] private float _xPosRight = 26.76f; // Posição em X para a casa da direita
    private float _lastZPos = 0f; // Posição Z da última casa gerada

    [SerializeField] private List<GameObject> _plots; // Lista de casas disponíveis para gerar

    private void Start()
    {
        PlotVerify(); // Verifica e gera as casas iniciais
    }

    private void PlotVerify()
    {
        for (int i = 0; i < _initAmount; i++)
        {
            SpawnPlot(); // Gera uma casa
        }
    }

    public void SpawnPlot()
    {
        int randomIndexLeft = GetRandomUniqueIndex(); // Obtém um índice aleatório único para a casa da esquerda
        int randomIndexRight = GetRandomUniqueIndex(); // Obtém um índice aleatório único para a casa da direita
        int randomIndexFront = GetRandomUniqueIndex(); // Obtém um índice aleatório único para a casa da frente

        GameObject plotLeft = _plots[randomIndexLeft]; // Obtém a casa da esquerda a partir do índice
        GameObject plotRight = _plots[randomIndexRight]; // Obtém a casa da direita a partir do índice
        GameObject plotFront = _plots[randomIndexFront]; // Obtém a casa da frente a partir do índice

        float zPos = _lastZPos + _plotSize; // Calcula a nova posição Z para a próxima casa

        Instantiate(plotLeft, new Vector3(_xPosLeft, 0, zPos), plotLeft.transform.rotation); // Instancia a casa da esquerda
        Instantiate(plotRight, new Vector3(_xPosRight, 0, zPos), Quaternion.Euler(0, 180, 0)); // Instancia a casa da direita
        Instantiate(plotFront, new Vector3(0, 0, zPos), plotFront.transform.rotation); // Instancia a casa da frente

        _lastZPos = zPos; // Atualiza a posição Z da última casa gerada
    }

    private int GetRandomUniqueIndex()
    {
        if (_plots.Count == 0)
        {
            Debug.LogWarning("No plots available to spawn."); // Exibe um aviso de depuração se não houver casas disponíveis
            return -1;
        }

        if (_plots.Count == 1)
            return 0; // Retorna o único índice disponível se houver apenas uma casa

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, _plots.Count); // Obtém um índice aleatório dentro do intervalo de índices disponíveis
        } while (randomIndex == lastUniqueIndex); // Repete o processo se o índice gerado for igual ao último índice único

        lastUniqueIndex = randomIndex; // Armazena o último índice único gerado

        return randomIndex; // Retorna o índice aleatório único
    }

    private int lastUniqueIndex = -1; // Índice da última casa gerada
}
