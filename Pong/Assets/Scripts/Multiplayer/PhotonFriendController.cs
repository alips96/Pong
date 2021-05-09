using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using PlayFabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System.Collections.Generic;
using System.Linq;

public class PhotonFriendController : MonoBehaviourPunCallbacks
{
    private PlayFabMaster PlayFabMaster;

    private void Start()
    {
        SetInitialReferences();

        PlayFabMaster.EventFriendsListUpdated += ManageFriendsList;
    }

    private void ManageFriendsList(List<PlayFabFriendInfo> friends)
    {
        if(friends.Count > 0)
        {
            string[] friendsDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendsDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendsInfoList = new List<PhotonFriendInfo>();
            PlayFabMaster.CallEventDisplayFriends(friendsInfoList);
        }
    }

    private void OnDestroy()
    {
        PlayFabMaster.EventFriendsListUpdated -= ManageFriendsList;
    }

    private void SetInitialReferences()
    {
        PlayFabMaster = transform.parent.GetComponent<PlayFabMaster>();
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        PlayFabMaster.CallEventDisplayFriends(friendList);
    }
}
