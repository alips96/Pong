using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Pun;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Text statusMessage;
    [SerializeField] private InputField usernameInput;

    [SerializeField] private GameObject usernamePanel;
    [SerializeField] private GameObject loginPanel;

    private PlayFabMaster playFabMasterScript;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "B07E4";
        }

        SetInitialReferences();
        CheckIfLeaveRoomRequired();
        CheckIfLoginReqired();
    }

    private void CheckIfLeaveRoomRequired() //we may jump from multiplayer to main menu.
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    private void CheckIfLoginReqired()
    {
        if (!PlayerPrefs.GetString("LOGGEDIN").Equals(""))
        {
            playFabMasterScript.CallEventUserLoggedIn(PlayerPrefs.GetString("LOGGEDIN"));
            PlayerPrefs.SetString("LOGGEDIN", "");
        }
    }

    private void SetInitialReferences()
    {
        playFabMasterScript = GetComponent<PlayFabMaster>();
    }

    public void RegisterPlayer() //Called by Sign Up Button.
    {
        if (passwordInput.text.Length < 6)
        {
            statusMessage.text = "Password should be at least 6 characters!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterationSuccess, OnError);
        statusMessage.text = "Please wait..";
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        statusMessage.text = error.ErrorMessage;
    }

    private void OnRegisterationSuccess(RegisterPlayFabUserResult result)
    {
        TogglePanels();
        statusMessage.text = "Registered and logged in!";
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
        statusMessage.text = "Please wait..";
    }

    private void OnUsernameCaptured(UpdateUserTitleDisplayNameResult result)
    {
        TogglePanels();
        playFabMasterScript.CallEventUserLoggedIn(result.DisplayName);
    }

    private void TogglePanels()
    {
        usernamePanel.SetActive(loginPanel.activeSelf);
        loginPanel.SetActive(!usernamePanel.activeSelf);
    }

    public void LoginPlayer() //Called by Login button.
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccessful, OnError);
        statusMessage.text = "Please wait..";
    }

    private void OnLoginSuccessful(LoginResult result)
    {
        statusMessage.text = string.Empty;
        Debug.Log("Logged in successfully!");

        playFabMasterScript.CallEventUserLoggedIn(result.InfoResultPayload.PlayerProfile.DisplayName);
    }

    public void ResetPassword() //Called by reset password button.
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "B07E4"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
        statusMessage.text = "Please wait..";
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        statusMessage.text = "Password sent to your email!";
    }
}
