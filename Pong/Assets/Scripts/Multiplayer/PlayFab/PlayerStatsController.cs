using UnityEngine;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using Zenject;
using Pong.MP.PlayFab;

public class PlayerStatsController : MonoBehaviour
{
    private bool shouldTakeAction = true;
    private PlayerStatsModel playerStatsModel;
    private PlayerStatsView playerStatsView;
    private int displayInterval = 3;

    [Inject]
    private void SetInitialReferences(PlayerStatsModel statsModel, PlayerStatsView statsView)
    {
        playerStatsModel = statsModel;
        playerStatsView = statsView;
    }

    public void GetStatsButtonClicked() //Called by get stats button.
    {
        if (!shouldTakeAction)
            return;

        shouldTakeAction = false;

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnPlayerDataReceived, OnError);
    }

    private void OnPlayerDataReceived(GetUserDataResult result)
    {
        playerStatsView.DisplayPanel();

        if (result.Data != null)
        {
            Dictionary<string, UserDataRecord> dataDic = result.Data;
            int winCount = 0, matchCount = 0;
            string text;

            if (dataDic.ContainsKey("Match"))
            {
                text = dataDic["Match"].Value;
                playerStatsView.totalGamesText.text = text;
                matchCount = Convert.ToInt32(text);
            }

            if (dataDic.ContainsKey("Win"))
            {
                text = dataDic["Win"].Value;
                playerStatsView.wonText.text = text;
                winCount = Convert.ToInt32(text);
            }

            if (dataDic.ContainsKey("Lose"))
            {
                playerStatsView.lossText.text = dataDic["Lose"].Value;
            }

            if (matchCount > 0)
            {
                playerStatsView.percentageText.text = playerStatsModel.CalculateWinPercentage(winCount, matchCount);
            }
        }

        StartCoroutine(DisableStatsPanel());
    }

    private IEnumerator DisableStatsPanel()
    {
        yield return new WaitForSeconds(displayInterval); //wait for a few seconds.

        playerStatsView.SwitchOffPanel();
        shouldTakeAction = true;
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}