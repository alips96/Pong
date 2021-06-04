using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private Text winnerText;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += SetWinnerText;
        PhotonNetwork.NetworkingClient.EventReceived += LoadScene;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= SetWinnerText;
        PhotonNetwork.NetworkingClient.EventReceived -= LoadScene;
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
            PhotonNetwork.RaiseEvent(4, 2, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
        }
        else
        {
            statusText.text = "The other player has left the room!";
        }
    }

    public void HomeButtonClicked() //Called by home button.
    {
        PlayerPrefs.SetString("LOGGEDIN", PhotonNetwork.LocalPlayer.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = false; //to make clients independent for a while.
            SceneManager.LoadScene(0);
        }
        else
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "and i am master client" + PhotonNetwork.IsMasterClient);
            StartCoroutine(GotoLoginScene());
        }

    }

    private IEnumerator GotoLoginScene()
    {
        yield return new WaitUntil(() => PhotonNetwork.IsMasterClient);
        PhotonNetwork.AutomaticallySyncScene = false; //to make clients independent for a while.
        SceneManager.LoadScene(0);
    }

    private void LoadScene(EventData obj)
    {
        if (obj.Code.Equals(4))
        {
            PhotonNetwork.LoadLevel((int)obj.CustomData); //Load scene.
        }
    }
}
