using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;

public class PlayFabMenu : MonoBehaviour
{
    private PlayFabMaster playFabMasterScript;

    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject mainMenu;

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
}
