using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine;
using Pong.General;
using Zenject;

namespace Pong.MP
{
    public class FriendsUI : MonoBehaviour
    {
        [SerializeField] private Text friendNameText;
        [SerializeField] private Image friendStatusImage;

        private FriendInfo friendInfo;

        private EventMaster eventMaster;
        //private Transform parentTransform;

        private void OnEnable()
        {
            eventMaster.EventToggleInvitationUI += ToggleInvitationButton;
            //transform.SetParent(parentTransform);
        }

        private void OnDisable()
        {
            eventMaster.EventToggleInvitationUI -= ToggleInvitationButton;
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
            //parentTransform = _parentTransform;
        }

        private void ToggleInvitationButton()
        {
            GameObject inviteButtonObject = transform.GetChild(2).gameObject; //getchild(2) will give us the invite button transform.
            inviteButtonObject.SetActive(!inviteButtonObject.activeSelf);
        }

        public void SetUI(FriendInfo friend)
        {
            friendInfo = friend;
            friendNameText.text = friend.UserId;

            if (friend.IsOnline)
            {
                friendStatusImage.color = Color.green;
            }
            else
            {
                friendStatusImage.color = Color.grey;
            }
        }

        public void InviteFriend() //Called by invite button
        {
            if (Photon.Pun.PhotonNetwork.InRoom)
            {
                Debug.Log("Inviting friend Action " + friendInfo.UserId);
                eventMaster.CallEventInviteFriend(friendInfo.UserId);
            }
            else
            {
                //connect to the room first
                Debug.Log("You should connect to a room first to be enable to invite your friends.");
            }
        }

        public void RemoveFriend() //Called by remove button
        {
            eventMaster.CallEventRemoveFriend(friendInfo.UserId);
        }

        public class Factory : PlaceholderFactory<FriendsUI> { }
    }
}