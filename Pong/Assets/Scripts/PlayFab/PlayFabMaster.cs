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
    public event UserEventHandler EventInviteFriend;
    public event UserEventHandler EventRoomInvitationAccepted;

    public delegate void InvitationsStatusEventHandler(InviteUI instance);
    public event InvitationsStatusEventHandler EventInviteAccepted;
    public event InvitationsStatusEventHandler EventInviteDeclined;

    public delegate void ScoreEventHandler(int score);
    public event ScoreEventHandler EventGameOver;

    public delegate void PlayFabFriendsEventHandler(List<FriendInfo> friends);
    public event PlayFabFriendsEventHandler EventFriendsListUpdated;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventGetPhotonFriends;

    public delegate void PhotonFriendsHandler(List<Photon.Realtime.FriendInfo> friendList);
    public event PhotonFriendsHandler EventDisplayFriends;

    public delegate void RoomInvitationEventHandler(string sender, string roomName);
    public event RoomInvitationEventHandler EventInvitedToTheRoom;
    public event RoomInvitationEventHandler EventDiscordJoinMessage;

    internal void CallEventRoomInvitationAccepted(string roomName)
    {
        EventRoomInvitationAccepted.Invoke(roomName);
    }

    internal void CallEventInviteAccepted(InviteUI instance)
    {
        EventInviteAccepted.Invoke(instance);
    }

    internal void CallEventInviteDeclined(InviteUI instance)
    {
        EventInviteDeclined.Invoke(instance);
    }

    internal void CallEventRemoveFriend(string userId)
    {
        EventRemoveFriend.Invoke(userId);
    }

    internal void CallEventInviteFriend(string userId)
    {
        EventInviteFriend.Invoke(userId);
    }

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

    internal void CallEventInvitedToTheRoom(string sender, string roomName)
    {
        EventInvitedToTheRoom.Invoke(sender, roomName);
    }

    internal void CallEventDiscordJoinMessage(string myName, string opponentName)
    {
        EventDiscordJoinMessage.Invoke(myName, opponentName);
    }
}
