using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class MP_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Transform ballTransform;
    
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += ApplyGameOver;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += ApplyGameOver;
    }

    private void ApplyGameOver(EventData obj)
    {
        if(obj.Code == 2)
        {
            Destroy(ballTransform.gameObject);
            gameOverMenu.SetActive(true);
        }
    }
}
