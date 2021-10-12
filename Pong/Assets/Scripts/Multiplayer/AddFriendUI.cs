using Pong.General;
using UnityEngine;
using Zenject;

namespace Pong.MP
{
    public class AddFriendUI : MonoBehaviour
    {
        [SerializeField] private string displayName;
        private EventMaster eventMaster;

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        public void SetAddFriendName(string someName) //Called by add friend input field.
        {
            displayName = someName;
        }

        public void AddFriend() //Called by add friend button
        {
            if (string.IsNullOrEmpty(displayName))
            {
                Debug.Log("Display name empty!!!");
                return;
            }

            eventMaster.CallEventAddFriend(displayName);
        }
    }
}