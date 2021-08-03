using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Pong.General;

namespace Pong.MP.Discord
{
    public class DiscordRoomHandler : MonoBehaviour
    {
        private EventMaster playFabMaster;

        private void OnEnable()
        {
            SetInitialReferenes();

            playFabMaster.EventDiscordJoinMessage += DisplayRoomNotification;
        }

        private void OnDisable()
        {
            playFabMaster.EventDiscordJoinMessage -= DisplayRoomNotification;
        }

        private void DisplayRoomNotification(string host, string guest)
        {
            //Remove this code to re enable bot notifications.
            //var request = new ExecuteCloudScriptRequest
            //{
            //    FunctionName = "matchKickedOff",
            //    FunctionParameter = new
            //    {
            //        hostName = host,
            //        guestName = guest
            //    }
            //};

            //PlayFabClientAPI.ExecuteCloudScript(request, OnMessageDisplayed, OnError);
        }

        private void OnMessageDisplayed(ExecuteCloudScriptResult result)
        {
            Debug.Log("Match kickoff message showed on discord!");
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }

        private void SetInitialReferenes()
        {
            playFabMaster = GetComponentInParent<EventMaster>();
        }
    }
}