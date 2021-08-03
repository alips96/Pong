using UnityEngine;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using System.Collections;

namespace Pong.MP.PlayFab
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private float alphaComponent = 0.3f;

        [SerializeField] private Text totalGamesText;
        [SerializeField] private Text wonText;
        [SerializeField] private Text lossText;
        [SerializeField] private Text percentageText;
        [SerializeField] private Transform statsPanel;

        private bool shouldTakeAction = true;
        [SerializeField] private int displayInterval = 3;

        public void GetStatsButtonClicked() //Called by get stats button.
        {
            if (!shouldTakeAction)
                return;

            shouldTakeAction = false;
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnPlayerDataReceived, OnError);
        }

        private void OnPlayerDataReceived(GetUserDataResult result)
        {
            transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, alphaComponent);
            statsPanel.gameObject.SetActive(true);

            if (result.Data != null)
            {
                Dictionary<string, UserDataRecord> dataDic = result.Data;
                int winCount = 0, matchCount = 0;

                if (dataDic.ContainsKey("Match"))
                {
                    totalGamesText.text = dataDic["Match"].Value;
                    matchCount = Convert.ToInt32(totalGamesText.text);
                }

                if (dataDic.ContainsKey("Win"))
                {
                    wonText.text = dataDic["Win"].Value;
                    winCount = Convert.ToInt32(wonText.text);
                }

                if (dataDic.ContainsKey("Lose"))
                {
                    lossText.text = dataDic["Lose"].Value;
                }

                if (matchCount > 0)
                {
                    percentageText.text = CalculateWinPercentage(winCount, matchCount);
                }
            }

            StartCoroutine(DisableStatsPanel());
        }

        public string CalculateWinPercentage(int winCount, int matchCount)
        {
            float percentage = winCount * 100f / matchCount;
            return ((float)Math.Round(percentage * 10f) / 10f).ToString(); //round this into one decimal point.
        }

        private IEnumerator DisableStatsPanel()
        {
            yield return new WaitForSeconds(displayInterval); //wait for a few seconds.

            transform.parent.GetComponent<Image>().color = Color.clear;
            statsPanel.gameObject.SetActive(false);
            shouldTakeAction = true;
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }
}