using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pong.General;
using Zenject;

namespace Pong.MP.PlayFab
{
    public class PlayFabFriendController : MonoBehaviour
    {
        private EventMaster eventMaster;
        private List<FriendInfo> friendsList;

        private void OnEnable()
        {
            SetInitialReferences();

            eventMaster.EventAddFriend += AddFrindToPlayFab;
            eventMaster.EventRemoveFriend += ApplyRemoveFriend;
            eventMaster.EventGetPhotonFriends += GetAllFriends;
        }

        private void OnDisable()
        {
            eventMaster.EventAddFriend -= AddFrindToPlayFab;
            eventMaster.EventRemoveFriend -= ApplyRemoveFriend;
            eventMaster.EventGetPhotonFriends -= GetAllFriends;
        }

        [Inject]
        private void SetScriptReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        private void SetInitialReferences()
        {
            friendsList = new List<FriendInfo>();
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
            eventMaster.CallEventFriendsListUpdated(friendsList);
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log(error.GenerateErrorReport());
        }
    }
}