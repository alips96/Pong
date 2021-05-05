using UnityEngine;

public class PlayFabMenu : MonoBehaviour
{
    private PlayFabMaster playFabMasterScript;

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

    private void SetupMainMenu()
    {
        Debug.Log("SetupSuccessfgullllllll");
    }
}
