using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollectableSpace : MonoBehaviour
{
    [SerializeField] private List<float> _collectableLanesX;
    [SerializeField] private List<float> _collectableJumpX;


    public float GetLane()
    {
        if(_collectableLanesX == null || _collectableLanesX.Count < 1)
        {
            return -20f;
        }

        return _collectableLanesX[Random.Range(0, _collectableLanesX.Count)];
    }
    
    public float GetJump()
    {
        if(_collectableJumpX == null || _collectableJumpX.Count < 1)
        {
            return -30f;
        }

        return _collectableJumpX[Random.Range(0, _collectableJumpX.Count)];
    }
}
