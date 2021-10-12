using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Pong.General;
using Zenject;

namespace Pong.MP.Discord
{
    public class DiscordLogin : MonoBehaviour
    {
        private EventMaster eventMaster;

        void OnEnable()
        {
            eventMaster.EventUserLoggedIn += DisplayLoginMessage;
        }

        private void OnDisable()
        {
            eventMaster.EventUserLoggedIn -= DisplayLoginMessage;
        }

        private void DisplayLoginMessage(string name)
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "userLoggedIn",
                FunctionParameter = new
                {
                    playerName = name
                }
            };

            PlayFabClientAPI.ExecuteCloudScript(request, OnMessageDisplayed, OnError);
        }

        private void OnMessageDisplayed(ExecuteCloudScriptResult result)
        {
            Debug.Log("Login message showed on discord!");
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }
    }
}