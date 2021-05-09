using System;
using UnityEngine;

public class AddFriendUI : MonoBehaviour
{
    [SerializeField] private string displayName;
    private PlayFabMaster PlayFabMaster;

    private void Start()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        PlayFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
    }

    public void SetAddFriendName(string someName)
    {
        displayName = someName;
    }

    public void AddFriend() //Called by add friend button
    {
        if (string.IsNullOrEmpty(displayName))
        {
            Debug.Log("Display name empty!!!");
            return;
        }

        PlayFabMaster.CallEventAddFriend(displayName);
    }
}
