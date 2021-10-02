using Pong.General;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pong.MP
{
    public class DisplayInvitationsUI : MonoBehaviour
    {
        private EventMaster eventMaster;
        private List<InviteUI> invitationsList;
        private InviteUI.Factory invitesUIFactory;

        [SerializeField] private Transform invitationsContainer;
        [SerializeField] private InviteUI uiInvitePrefab;
        [SerializeField] private RectTransform contentRect;
        [SerializeField] private Vector2 originalSize;
        [SerializeField] private Vector2 increasedSize;

        private void OnEnable()
        {
            SetInitialReferences();

            eventMaster.EventInvitedToTheRoom += HandleRoomInvitation;
            eventMaster.EventInviteAccepted += InviteAccepted;
            eventMaster.EventInviteDeclined += InviteDeclined;
        }

        private void OnDisable()
        {
            eventMaster.EventInvitedToTheRoom -= HandleRoomInvitation;
            eventMaster.EventInviteAccepted -= InviteAccepted;
            eventMaster.EventInviteDeclined -= InviteDeclined;
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

            InviteUI uiInvite = invitesUIFactory.Create();
            uiInvite.transform.SetParent(invitationsContainer);
            uiInvite.Initialize(friend, roomName);
            contentRect.sizeDelta += increasedSize;
            invitationsList.Add(uiInvite);
        }

        private void SetInitialReferences()
        {
            contentRect = invitationsContainer.GetComponent<RectTransform>();
            originalSize = contentRect.sizeDelta;
            increasedSize = new Vector2(0, uiInvitePrefab.GetComponent<RectTransform>().sizeDelta.y);
            invitationsList = new List<InviteUI>();
        }

        [Inject]
        private void SetScriptReferences(EventMaster _eventMaster, InviteUI.Factory _invitesUIFactory)
        {
            eventMaster = _eventMaster;
            invitesUIFactory = _invitesUIFactory;
        }
    }
}