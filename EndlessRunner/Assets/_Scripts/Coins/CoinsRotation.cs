using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotation : MonoBehaviour
{
   [SerializeField] private float _rotationSpeed = 115;
    // Update is called once per frame
    void Update()
    {
        CoinRotation();
    }

    private void CoinRotation()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
