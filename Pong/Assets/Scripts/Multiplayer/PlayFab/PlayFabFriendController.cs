using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayFabFriendController : MonoBehaviour
{
    private PlayFabMaster playFabMaster;
    private List<FriendInfo> friendsList;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventAddFriend += AddFrindToPlayFab;
        playFabMaster.EventRemoveFriend += ApplyRemoveFriend;
        playFabMaster.EventGetPhotonFriends += GetPhotonFriends;
    }

    private void OnDisable()
    {
        playFabMaster.EventAddFriend -= AddFrindToPlayFab;
        playFabMaster.EventRemoveFriend -= ApplyRemoveFriend;
        playFabMaster.EventGetPhotonFriends -= GetPhotonFriends;
    }

    private void GetPhotonFriends()
    {
        GetAllFriends();
    }

    private void SetInitialReferences()
    {
        friendsList = new List<FriendInfo>();
        playFabMaster = GetComponent<PlayFabMaster>();
    }

    private void ApplyRemoveFriend(string name)
    {
        string id = friendsList.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId.ToString();

        var request = new RemoveFriendRequest
        {
            FriendPlayFabId = id
        };

        PlayFabClientAPI.RemoveFriend(request, OnRemovedFriend, OnError);
    }

    private void OnRemovedFriend(RemoveFriendResult result)
    {
        GetAllFriends();
    }

    private void AddFrindToPlayFab(string name)
    {
        var request = new AddFriendRequest
        {
            FriendTitleDisplayName = name
        };

        PlayFabClientAPI.AddFriend(request, OnFriendAdded, OnError);
    }

    private void OnFriendAdded(AddFriendResult result)
    {
        GetAllFriends();
    }

    private void GetAllFriends()
    {
        var request = new GetFriendsListRequest
        {
            IncludeFacebookFriends = false,
            IncludeSteamFriends = false,
            XboxToken = null
        };

        PlayFabClientAPI.GetFriendsList(request, OnGetFriendsSuccess, OnError);
    }

    private void OnGetFriendsSuccess(GetFriendsListResult result)
    {
        friendsList = result.Friends;
        playFabMaster.CallEventFriendsListUpdated(friendsList);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
