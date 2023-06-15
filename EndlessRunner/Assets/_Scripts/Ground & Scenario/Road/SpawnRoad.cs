using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads;
    private float _roadOffset = 23f;

    // Start is called before the first frame update
    void Start()
    {
        AddRoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddRoads()
    {
        if(_roads.Count > 0)
        {
            _roads = _roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoads()
    {
        GameObject moveRoads = _roads[0];
        _roads.RemoveAt(0);
        _roads.Add(moveRoads);
        
        float newZ = _roads[_roads.Count - 1].transform.position.z + _roadOffset;
        moveRoads.transform.position = new Vector3(0, 0 , newZ);
    }
}
