using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine;
using Pong.General;

namespace Pong.MP
{
    public class FriendsUI : MonoBehaviour
    {
        [SerializeField] private Text friendNameText;
        [SerializeField] private Image friendStatusImage;

        private FriendInfo friendInfo;

        private EventMaster playFabMaster;

        private void Start()
        {
            playFabMaster = GameObject.Find("Network Manager").GetComponent<EventMaster>();
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
                Debug.Log("Inviting friend Action" + friendInfo.UserId);
                playFabMaster.CallEventInviteFriend(friendInfo.UserId);
            }
            else
            {
                //connect to the room first
                Debug.Log("You should connect to a room first to be enable to invite your friends.");
            }
        }

        public void RemoveFriend() //Called by remove button
        {
            playFabMaster.CallEventRemoveFriend(friendInfo.UserId);
        }
    }
}