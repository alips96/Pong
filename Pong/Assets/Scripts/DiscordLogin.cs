using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class DiscordLogin : MonoBehaviour
{
    private PlayFabMaster playFabMaster;

    void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventUserLoggedIn += DisplayLoginMessage;
    }

    private void OnDisable()
    {
        playFabMaster.EventUserLoggedIn -= DisplayLoginMessage;
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

    private void SetInitialReferences()
    {
        playFabMaster = transform.parent.GetComponent<PlayFabMaster>();
    }
}
