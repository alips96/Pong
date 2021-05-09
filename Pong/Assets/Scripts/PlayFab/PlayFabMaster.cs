using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabMaster : MonoBehaviour
{
    public delegate void UserEventHandler(string name);
    public event UserEventHandler EventUserLoggedIn;
    public event UserEventHandler EventAddFriend;
    public event UserEventHandler EventRemoveFriend;

    public delegate void ScoreEventHandler(int score);
    public event ScoreEventHandler EventGameOver;

    public delegate void PlayFabFriendsEventHandler(List<FriendInfo> friends);
    public event PlayFabFriendsEventHandler EventFriendsListUpdated;

    public delegate void GeneralEventHandler();

    internal void CallEventRemoveFriend(string userId)
    {
        EventRemoveFriend.Invoke(userId);
    }

    public event GeneralEventHandler EventGetPhotonFriends;

    public delegate void PhotonFriendsHandler(List<Photon.Realtime.FriendInfo> friendList);
    public event PhotonFriendsHandler EventDisplayFriends;

    public void CallEventUserLoggedIn(string displayName)
    {
        EventUserLoggedIn.Invoke(displayName);
    }

    public void CallEventGameOver(int score)
    {
        EventGameOver.Invoke(score);
    }

    public void CallEventAddFriend(string friendName)
    {
        EventAddFriend.Invoke(friendName);
    }

    internal void CallEventFriendsListUpdated(List<FriendInfo> friends)
    {
        EventFriendsListUpdated.Invoke(friends);
    }

    internal void CallEventDisplayFriends(List<Photon.Realtime.FriendInfo> friendList)
    {
        EventDisplayFriends.Invoke(friendList);
    }

    internal void CallEventGetPhotonFriends()
    {
        EventGetPhotonFriends.Invoke();
    }
}
