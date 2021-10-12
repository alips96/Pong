using Photon.Realtime;
using Pong.General;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pong.MP
{
    public class DisplayFriendsUI : MonoBehaviour
    {
        private EventMaster eventMaster;

        [SerializeField] private Transform friendContainer;
        private FriendsUI.Factory friendsUIFactory;

        private void OnEnable()
        {
            eventMaster.EventDisplayFriends += DisplayFriends;
        }

        private void OnDisable()
        {
            eventMaster.EventDisplayFriends -= DisplayFriends;
        }

        private void DisplayFriends(List<FriendInfo> friendList)
        {
            foreach (Transform child in friendContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (FriendInfo item in friendList)
            {
                //Instantiating friends ui prefab and pass the friendcontainer as its parent.
                FriendsUI friendUI = friendsUIFactory.Create(friendContainer);

                friendUI.SetUI(item);
            }
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster, FriendsUI.Factory _friendsUIFactory)
        {
            eventMaster = _eventMaster;
            friendsUIFactory = _friendsUIFactory;
        }
    }
}