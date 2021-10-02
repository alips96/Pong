using Pong.General;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.MP
{
    public class InviteUI : MonoBehaviour
    {
        private string friendName;
        private string roomName;
        [SerializeField] private Text friendNameText;

        private EventMaster eventMaster;

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
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

            if(eventMaster == null)
            {
                Debug.LogError("NULL Unfortunately");
            }
            eventMaster.CallEventInviteAccepted(this);
            eventMaster.CallEventRoomInvitationAccepted(roomName);
        }

        public void DeclineInvite() //Called by remove button
        {
            Debug.Log("Decline Invite Action");
            eventMaster.CallEventInviteDeclined(this);
        }

        public class Factory : PlaceholderFactory<InviteUI> { }
    }
}