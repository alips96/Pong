using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine;
using Pong.General;
using Zenject;
using Photon.Pun;

namespace Pong.MP
{
    public class FriendsUI : MonoBehaviour
    {
        [SerializeField] private Text friendNameText;
        [SerializeField] private Image friendStatusImage;

        private FriendInfo friendInfo;

        private EventMaster eventMaster;
        private Transform parentTransform;

        private GameObject inviteButtonObject;

        private void OnEnable()
        {
            eventMaster.EventToggleInvitationUI += CheckToActivateInviteButton;

            CheckToActivateInviteButton();
        }

        private void OnDisable()
        {
            eventMaster.EventToggleInvitationUI -= CheckToActivateInviteButton;

            CheckToActivateInviteButton();
        }

        private void CheckToActivateInviteButton()
        {
            if (PhotonNetwork.InRoom)
            {
                inviteButtonObject.SetActive(!inviteButtonObject.activeSelf);
            }
            else
            {
                inviteButtonObject.SetActive(false);
            }
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster, Transform _parentTransform)
        {
            eventMaster = _eventMaster;
            parentTransform = _parentTransform;

            transform.SetParent(parentTransform);
            inviteButtonObject = transform.GetChild(2).gameObject;
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
            if (PhotonNetwork.InRoom)
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

        public class Factory : PlaceholderFactory<Transform, FriendsUI> { }
    }
}