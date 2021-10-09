using Photon.Pun;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pong.General;
using Zenject;

namespace Pong.MP
{
    public class PhotonFriendController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float refreshCooldown = 15f;

        private float refreshCountdown = 0f;
        public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate { };

        [SerializeField] private GameObject friendsContainer;

        private EventMaster eventMaster;
        private PhotonFriendModel photonFriendModel;

        private void Start()
        {
            eventMaster.EventFriendsListUpdated += photonFriendModel.HandleFriendsUpdated;
        }

        private void OnDestroy()
        {
            eventMaster.EventFriendsListUpdated -= photonFriendModel.HandleFriendsUpdated;
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster, PhotonFriendModel friendModel)
        {
            eventMaster = _eventMaster;
            photonFriendModel = friendModel;
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

                if (PhotonNetwork.InRoom)
                {
                    return;
                }

                if (friendsContainer.activeInHierarchy)
                    photonFriendModel.FindPhotonFriends();
            }
        }

        public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
        {
            Debug.Log($"Invoke UI to display Photon friends found: {friendList.Count}");

            if (friendsContainer.activeInHierarchy)
                eventMaster.CallEventDisplayFriends(friendList);
        }
    }
}