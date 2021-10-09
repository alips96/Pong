using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System;
using Photon.Pun;
using Pong.General;
using System.Linq;

namespace Pong.MP
{
    public class PhotonFriendModel
    {
        public List<PlayfabFriendInfo> friendsList;
        private EventMaster eventMaster;

        public PhotonFriendModel(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;

            friendsList = new List<PlayfabFriendInfo>();
        }

        public void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
        {
            friendsList = friends;
            FindPhotonFriends(friendsList);
        }

        public void FindPhotonFriends(List<PlayfabFriendInfo> friends)
        {
            Debug.Log($"Handle getting Photon friends: {friends.Count}");

            if (friends.Count != 0)
            {
                string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();

                if (PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.InRoom && PhotonNetwork.InLobby)
                    PhotonNetwork.FindFriends(friendDisplayNames);
            }
            else
            {
                List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
                eventMaster.CallEventDisplayFriends(friendList);
            }
        }
    }
}