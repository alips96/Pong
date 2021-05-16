using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private Text winnerText;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += SetWinnerText;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += SetWinnerText;
    }

    private void SetWinnerText(EventData obj)
    {
        if (obj.Code != 2)
            return;

        if ((bool)obj.CustomData) //master player Won
        {
            if (PhotonNetwork.IsMasterClient)
            {
                winnerText.text = "You won!";
                winnerText.color = Color.green;
            }
            else
            {
                winnerText.text = "You lost :(";
                winnerText.color = Color.red;
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                winnerText.text = "You lost :(";
                winnerText.color = Color.red;
            }
            else
            {
                winnerText.text = "You won!";
                winnerText.color = Color.green;
            }
        }
    }

    public void ReplayButtonClicked() //Called by replay button
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount.Equals(2))
        {
            PhotonNetwork.LoadLevel(2); //Load Multiplayer scene.
        }
        else
        {
            statusText.text = "the other player has left the room!";
        }
    }

    public void HomeButtonClicked() //Called by home button.
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(1);
    }
}
