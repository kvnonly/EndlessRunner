using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads; // Lista de estradas disponíveis
    [SerializeField] private float _roadOffset = 20f; // Distância entre as estradas
    [SerializeField] private float _spawnInX;
    [SerializeField] private float _spawnInY;

    private void Start()
    {
        AddRoads(); // Adiciona as estradas e as organiza
    }

    private void AddRoads()
    {
        _roads = _roads.OrderBy(r => r.transform.position.z).ToList(); // Organiza as estradas em ordem crescente de posição Z
    }

public void MoveRoads()
{
    GameObject moveRoad = _roads[0]; // Obtém a primeira estrada da lista
    _roads.RemoveAt(0); // Remove a primeira estrada da lista

    float newZ = _roads[_roads.Count - 1].transform.position.z + _roadOffset; // Calcula a nova posição Z para a estrada movida
    moveRoad.transform.position = new Vector3(_spawnInX, _spawnInY, newZ); // Define a nova posição para a estrada movida

    _roads.Add(moveRoad); // Adiciona a estrada removida ao final da lista

    //Debug.Log("MoveRoads: Estrada movida para a posição Z " + newZ);
}
}
