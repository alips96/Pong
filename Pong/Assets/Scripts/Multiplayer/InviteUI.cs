using Pong.General;
using UnityEngine;
using UnityEngine.UI;

namespace Pong.MP
{
    public class InviteUI : MonoBehaviour
    {
        private string friendName;
        private string roomName;
        [SerializeField] private Text friendNameText;

        private EventMaster playFabMaster;

        private void Start()
        {
            playFabMaster = GameObject.Find("Network Manager").GetComponent<EventMaster>();
        }

        public void Initialize(string friend, string room)
        {
            friendName = friend;
            roomName = room;

            friendNameText.text = friendName;
        }

        public void AcceptInvite() //Called by invite button
        {
            Debug.Log("Accept Invite Action");
            playFabMaster.CallEventInviteAccepted(this);
            playFabMaster.CallEventRoomInvitationAccepted(roomName);
        }

        public void DeclineInvite() //Called by remove button
        {
            Debug.Log("Decline Invite Action");
            playFabMaster.CallEventInviteDeclined(this);
        }
    }
}