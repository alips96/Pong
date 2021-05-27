using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class DiscordRoomHandler : MonoBehaviour
{
    private PlayFabMaster playFabMaster;

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
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "matchKickedOff",
            FunctionParameter = new
            {
                hostName = host,
                guestName = guest
            }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnMessageDisplayed, OnError);
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
        playFabMaster = GetComponentInParent<PlayFabMaster>();
    }
}
