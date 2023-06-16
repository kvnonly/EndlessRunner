using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads; // Lista de estradas disponíveis
    private float _roadOffset = 24f; // Distância entre as estradas

    void Start()
    {
        AddRoads(); // Adiciona as estradas e as organiza
    }

    private void AddRoads()
    {
        if (_roads.Count > 0)
        {
            _roads = _roads.OrderBy(r => r.transform.position.z).ToList(); // Organiza as estradas em ordem crescente de posição Z
        }
    }

    public void MoveRoads()
    {
        GameObject moveRoad = _roads[0]; // Obtém a primeira estrada da lista
        _roads.RemoveAt(0); // Remove a primeira estrada da lista
        _roads.Add(moveRoad); // Adiciona a estrada removida ao final da lista

        float newZ = _roads[_roads.Count - 1].transform.position.z + _roadOffset; // Calcula a nova posição Z para a estrada movida
        moveRoad.transform.position = new Vector3(0, 0, newZ); // Define a nova posição para a estrada movida
    }
}
