using System;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine;

public class FriendsUI : MonoBehaviour
{
    [SerializeField] private Text friendNameText;
    [SerializeField] private FriendInfo friendInfo;
    [SerializeField] private Image friendStatusImage;

    private PlayFabMaster playFabMaster;

    private void Start()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
    }

    public void SetUI(FriendInfo friend)
    {
        friendInfo = friend;
        friendNameText.text = friend.UserId;

        if (friend.IsOnline)
        {
            friendStatusImage.color = Color.green;
        }
        else
        {
            friendStatusImage.color = Color.grey;
        }
    }

    public void InviteFriend() //Called by invite button
    {
        Debug.Log("Inviting friend Action" + friendInfo.UserId);
        playFabMaster.CallEventInviteFriend(friendInfo.UserId);
    }

    public void RemoveFriend() //Called by remove button
    {
        playFabMaster.CallEventRemoveFriend(friendInfo.UserId);
    }
    
}
