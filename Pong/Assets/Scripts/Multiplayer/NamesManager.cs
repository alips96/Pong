using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Pong.MP
{
    public class NamesManager : MonoBehaviour
    {
        [SerializeField] private Text playerText;
        [SerializeField] private Text opponentText;

        private Player[] playersArr;

        private void Start()
        {
            playersArr = PhotonNetwork.PlayerList;

            if (playersArr.Length == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                AssignUINames();
            }
            else
            {
                Debug.LogError("Maximum room capacity is not reached:");
            }
        }

        private void AssignUINames()
        {
            playerText.text = playersArr[0].NickName;
            opponentText.text = playersArr[1].NickName;
        }
    }
}