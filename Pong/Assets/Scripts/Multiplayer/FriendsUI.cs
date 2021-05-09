using System;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine;

public class FriendsUI : MonoBehaviour
{
    [SerializeField] private Text friendNameText;
    [SerializeField] private FriendInfo friendInfo;

    private PlayFabMaster playFabMaster;

    private void Start()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<PlayFabMaster>();
    }

    public void SetInitialReferences(FriendInfo friend)
    {
        friendInfo = friend;
        friendNameText.text = friend.UserId;
    }

    public void RemoveFriend()
    {
        playFabMaster.CallEventRemoveFriend(friendInfo.UserId);
    }
    
}
