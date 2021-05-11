using UnityEngine;
using UnityEngine.UI;

public class InviteUI : MonoBehaviour
{
    private string friendName;
    private string roomName;
    [SerializeField] private Text friendNameText;

    private PlayFabMaster playFabMaster;

    private void Start()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
    }

    public void Initialize(string friend, string room)
    {
        friendName = friend;
        roomName = room;

        friendNameText.text = friendName;
    }

    public void AcceptInvite() //Called by invite button
    {
        Debug.Log("Accept Invite Action");
        playFabMaster.CallEventInviteAccepted(this);
        playFabMaster.CallEventRoomInvitationAccepted(roomName);
    }

    public void DeclineInvite() //Called by remove button
    {
        Debug.Log("Decline Invite Action");
        playFabMaster.CallEventInviteDeclined(this);
    }
}
