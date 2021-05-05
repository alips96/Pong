using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabMenu : MonoBehaviour
{
    private PlayFabMaster playFabMasterScript;

    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private InputField usernameInput;

    [SerializeField] private Text username;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMasterScript.EventUserLoggedIn += SetupMainMenu;
    }

    private void OnDisable()
    {
        playFabMasterScript.EventUserLoggedIn -= SetupMainMenu;
    }

    private void SetInitialReferences()
    {
        playFabMasterScript = GetComponent<PlayFabMaster>();
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
        username.text = result.DisplayName;
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
