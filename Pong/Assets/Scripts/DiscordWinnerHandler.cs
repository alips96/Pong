using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Realtime;

public class DiscordWinnerHandler : MonoBehaviour
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += HandleDiscordWinner;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= HandleDiscordWinner;
    }

    private void HandleDiscordWinner(EventData obj)
    {
        if (!obj.Code.Equals(2)) //game over event
            return;

        if (!PhotonNetwork.IsMasterClient)
            return;

        Player[] roomPlayers = PhotonNetwork.PlayerList;

        if (roomPlayers.Length.Equals(2))
        {
            if ((bool)obj.CustomData) //I won
            {
                SendDiscordNotif(roomPlayers[0].NickName, roomPlayers[1].NickName);
            }
            else //opponent won
            {
                SendDiscordNotif(roomPlayers[1].NickName, roomPlayers[0].NickName);
            }
        }
        else
        {
            Debug.LogWarning("Current room player count: " + roomPlayers.Length + " ,so the discord notif won't be sent!");
        }
    }

    private void SendDiscordNotif(string winner, string loser)
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "announceWinner",
            FunctionParameter = new
            {
                winnerName = winner,
                loserName = loser
            }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnWinnerAnnounced, OnError);
    }

    private void OnWinnerAnnounced(ExecuteCloudScriptResult result)
    {
        Debug.Log("winner displayed on discord!");
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
