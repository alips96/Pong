using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Text statusMessage;

    public void RegisterPlayer() //Called by Sign Up Button.
    {
        if(passwordInput.text.Length < 6)
        {
            statusMessage.text = "Password should be at least 6 characters!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterationSuccess, OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        statusMessage.text = error.ErrorMessage;
    }

    private void OnRegisterationSuccess(RegisterPlayFabUserResult result)
    {
        statusMessage.text = "Registered and Logged in";
    }

    public void LoginPlayer() //Called by Login button.
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccessful, OnError);
    }

    private void OnLoginSuccessful(LoginResult result)
    {
        statusMessage.text = "Logged in!";
        Debug.Log("Logged in successfully!");
    }

    public void ResetPassword() //Called by reset password button.
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "B07E4"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        statusMessage.text = "Password sent to your email!";
    }
}
