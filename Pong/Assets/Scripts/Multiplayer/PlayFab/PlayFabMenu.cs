using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Pong.General;
using Zenject;

namespace Pong.MP.PlayFab
{
    public class PlayFabMenu : MonoBehaviour
    {
        private EventMaster eventMaster;

        [SerializeField] private GameObject loginMenu;
        [SerializeField] private GameObject mainMenu;

        [SerializeField] private InputField usernameInput;

        [SerializeField] private Text username;

        private void OnEnable()
        {
            eventMaster.EventUserLoggedIn += SetupMainMenu;
        }

        private void OnDisable()
        {
            eventMaster.EventUserLoggedIn -= SetupMainMenu;
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        private void SetupMainMenu(string displayName)
        {
            ToggleMenus();
            username.text = displayName;
        }

        private void ToggleMenus()
        {
            loginMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void SetUsername() //Called by Username input field
        {
            if (string.IsNullOrWhiteSpace(usernameInput.text))
                return;

            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = usernameInput.text
            };

            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUsernameCaptured, OnError);
        }

        private void OnUsernameCaptured(UpdateUserTitleDisplayNameResult result)
        {
            string displayName = result.DisplayName;

            Photon.Pun.PhotonNetwork.NickName = displayName;
            username.text = displayName;
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log(error.GenerateErrorReport());
        }
    }
}