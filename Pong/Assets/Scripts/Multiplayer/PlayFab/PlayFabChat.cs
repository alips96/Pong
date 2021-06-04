using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabChat : MonoBehaviour
{
    private InputField myInputField;
    private Transform parent;

    [SerializeField] private Text displayNameText;

    private void Start()
    {
        SetInitialReferences();
    }

    public void OnUserSentNotif() //Called by chat input field.
    {
        if (!string.IsNullOrEmpty(myInputField.text))
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "sendNotification",
                FunctionParameter = new
                {
                    author = displayNameText.text,
                    body = myInputField.text
                }
            };

            PlayFabClientAPI.ExecuteCloudScript(request, OnMessageSent, OnError);
        }

        ManageUI();
    }

    private void OnMessageSent(ExecuteCloudScriptResult result)
    {
        Debug.Log("Successfully sent message to the discord server!");
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void ManageUI()
    {
        myInputField.text = string.Empty;
        parent.gameObject.SetActive(false);
    }

    private void SetInitialReferences()
    {
        myInputField = transform.GetComponent<InputField>();
        parent = transform.parent;
    }
}
