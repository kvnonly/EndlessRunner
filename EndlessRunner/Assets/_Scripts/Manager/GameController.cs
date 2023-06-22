using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private TMP_Text _distance;
    [SerializeField] private TMP_Text _coins;

    private int _playerCoins;

    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatingDistanceText()
    {
        int distance = Mathf.RoundToInt(_player.transform.position.z);
        _distance.text = distance.ToString() + "m";
    }
    private void UpdatingCoinsText()
    {
        _coins.text = _playerCoins.ToString();
    }

    public void CoinsCollected()
    {
        _playerCoins++;
    }
}
