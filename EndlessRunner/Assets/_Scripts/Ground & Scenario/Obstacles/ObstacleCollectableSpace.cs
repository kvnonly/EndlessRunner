using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollectableSpace : MonoBehaviour
{
    [SerializeField] private List<float> _collectableLanesX;  // Lista de lanes (posições X) onde os objetos podem ser coletados
    [SerializeField] private List<float> _collectableJumpX;  // Lista de posições X onde os objetos podem ser pulados

    // Retorna uma posição de lane aleatória onde um objeto pode ser coletado
    public float GetLane()
    {
        if (_collectableLanesX == null || _collectableLanesX.Count < 1)
        {
            return -20f;  // Retorna -20f se a lista de lanes estiver vazia ou nula
        }

        return _collectableLanesX[Random.Range(0, _collectableLanesX.Count)];  // Retorna uma posição aleatória da lista de lanes
    }
    
    // Retorna uma posição de salto aleatória onde um objeto pode ser pulado
    public float GetJump()
    {
        if (_collectableJumpX == null || _collectableJumpX.Count < 1)
        {
            return -30f;  // Retorna -30f se a lista de posições de salto estiver vazia ou nula
        }

        return _collectableJumpX[Random.Range(0, _collectableJumpX.Count)];  // Retorna uma posição aleatória da lista de posições de salto
    }
}
