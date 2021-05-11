using Photon.Pun;
using Photon.Realtime;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PhotonFriendController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float refreshCooldown = 15f;
    [SerializeField]private float refreshCountdown = 0f;
    [SerializeField] private List<PlayfabFriendInfo> friendList;
    public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate { };

    [SerializeField] private GameObject friendsContainer;

    private PlayFabMaster playFabMaster;

    private void Start()
    {
        SetInitialReferences();
        friendList = new List<PlayfabFriendInfo>();
        playFabMaster.EventFriendsListUpdated += HandleFriendsUpdated;
    }

    private void OnDestroy()
    {
        playFabMaster.EventFriendsListUpdated -= HandleFriendsUpdated;
    }

    private void SetInitialReferences()
    {
        playFabMaster = transform.parent.GetComponent<PlayFabMaster>();
    }

    private void Update()
    {
        if (refreshCountdown > 0)
        {
            refreshCountdown -= Time.deltaTime;
        }
        else
        {
            refreshCountdown = refreshCooldown;
            if (PhotonNetwork.InRoom) return;

            if(friendsContainer.activeInHierarchy)
                FindPhotonFriends(friendList);
        }
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        friendList = friends;
        FindPhotonFriends(friendList);
    }

    private void FindPhotonFriends(List<PlayfabFriendInfo> friends)
    {
        Debug.Log($"Handle getting Photon friends: {friends.Count}");
        if (friends.Count != 0)
        {
            string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
            playFabMaster.CallEventDisplayFriends(friendList);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        Debug.Log($"Invoke UI to display Photon friends found: {friendList.Count}");
        playFabMaster.CallEventDisplayFriends(friendList);
    }
}