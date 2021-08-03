using Pong.General;
using UnityEngine;

namespace Pong.MP
{
    public class AddFriendUI : MonoBehaviour
    {
        [SerializeField] private string displayName;
        private EventMaster PlayFabMaster;

        private void Start()
        {
            SetInitialReferences();
        }

        private void SetInitialReferences()
        {
            PlayFabMaster = GameObject.Find("Network Manager").GetComponent<EventMaster>();
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

            PlayFabMaster.CallEventAddFriend(displayName);
        }
    }
}