using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInvitationsUI : MonoBehaviour
{
    private PlayFabMaster playFabMaster;
    private List<InviteUI> invitationsList;

    [SerializeField] private Transform invitationsContainer;
    [SerializeField] private InviteUI uiInvitePrefab;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private Vector2 originalSize;
    [SerializeField] private Vector2 increasedSize;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventInvitedToTheRoom += HandleRoomInvitation;
        playFabMaster.EventInviteAccepted += InviteAccepted;
        playFabMaster.EventInviteDeclined += InviteDeclined;
    }

    private void OnDisable()
    {
        playFabMaster.EventInvitedToTheRoom -= HandleRoomInvitation;
        playFabMaster.EventInviteAccepted -= InviteAccepted;
        playFabMaster.EventInviteDeclined -= InviteDeclined;
    }

    private void InviteAccepted(InviteUI invite)
    {
        if (invitationsList.Contains(invite))
        {
            invitationsList.Remove(invite);
            Destroy(invite.gameObject);
        }
    }

    private void InviteDeclined(InviteUI invite)
    {
        if (invitationsList.Contains(invite))
        {
            invitationsList.Remove(invite);
            Destroy(invite.gameObject);
        }
    }

    private void HandleRoomInvitation(string friend, string roomName)
    {
        Debug.Log("friend: " + friend + " invited to the room: " + roomName);

        InviteUI uiInvite = Instantiate(uiInvitePrefab, invitationsContainer);
        uiInvite.Initialize(friend, roomName);
        contentRect.sizeDelta += increasedSize;
        invitationsList.Add(uiInvite);
    }

    private void SetInitialReferences()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
        contentRect = invitationsContainer.GetComponent<RectTransform>();
        originalSize = contentRect.sizeDelta;
        increasedSize = new Vector2(0, uiInvitePrefab.GetComponent<RectTransform>().sizeDelta.y);
        invitationsList = new List<InviteUI>();
    }
}
