using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace Pong.MP.PlayFab
{
    public class PlayerStats : MonoBehaviour
    {
        private int lossCount, winCount, matchCount;
        private bool hasMasterPlayerWon;

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += GetPlayerData;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= GetPlayerData;
        }

        private void GetPlayerData(EventData obj)
        {
            if (!obj.Code.Equals(2)) //game over event
                return;

            hasMasterPlayerWon = (bool)obj.CustomData;
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnPlayerDataReceived, OnError);
        }

        private void OnPlayerDataReceived(GetUserDataResult result)
        {
            lossCount = GetLossCount(result);
            winCount = GetWinCount(result);
            matchCount = GetTotalMatchCount(result);

            SetWinner();
        }

        private void SetWinner()
        {
            bool isMasterClient = PhotonNetwork.IsMasterClient;

            if (hasMasterPlayerWon)
            {
                if (isMasterClient)
                {
                    IncrementWonCount();
                }
                else
                {
                    IncrementLoseCount();
                }
            }
            else
            {
                if (isMasterClient)
                {
                    IncrementLoseCount();
                }
                else
                {
                    IncrementWonCount();
                }
            }
        }

        private void IncrementLoseCount()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
            {
                {"Lose", (++lossCount).ToString() },
                {"Match", (++matchCount).ToString() }
            }
            };

            PlayFabClientAPI.UpdateUserData(request, OnLostDataSent, OnError);
        }

        private void IncrementWonCount()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
            {
                {"Win", (++winCount).ToString() },
                {"Match", (++matchCount).ToString() }
            }
            };

            PlayFabClientAPI.UpdateUserData(request, OnWinCountSent, OnError);
        }

        private void OnWinCountSent(UpdateUserDataResult result)
        {
            Debug.Log("Player win count incremented successfully");
        }

        private int GetWinCount(GetUserDataResult result)
        {
            if (result.Data.ContainsKey("Win"))
            {
                return Convert.ToInt32(result.Data["Win"].Value);
            }

            return 0;
        }

        private int GetTotalMatchCount(GetUserDataResult result)
        {
            if (result.Data.ContainsKey("Match"))
            {
                return Convert.ToInt32(result.Data["Match"].Value);
            }

            return 0;
        }

        private int GetLossCount(GetUserDataResult result)
        {
            if (result.Data.ContainsKey("Lose"))
            {
                return Convert.ToInt32(result.Data["Lose"].Value);
            }

            return 0;
        }

        private void OnLostDataSent(UpdateUserDataResult result)
        {
            Debug.Log("Player loss count incremented successfully");
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }
}