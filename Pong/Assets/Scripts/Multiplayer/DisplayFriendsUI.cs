using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFriendsUI : MonoBehaviour
{
    private PlayFabMaster playFabMaster;

    [SerializeField] private Transform friendContainer;
    [SerializeField] private FriendsUI friendPrefab;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventDisplayFriends += DisplayFriends;
    }



    private void OnDisable()
    {
        playFabMaster.EventDisplayFriends -= DisplayFriends;
    }

    private void DisplayFriends(List<FriendInfo> friendList)
    {
        foreach (Transform child in friendContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (FriendInfo item in friendList)
        {
            FriendsUI friendUI = Instantiate(friendPrefab, friendContainer);
            friendUI.SetUI(item);
        }
    }

    private void SetInitialReferences()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
    }
}
