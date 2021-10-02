using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Pong.General;
using Zenject;

namespace Pong.SP
{
    public class SP_Leaderboard : MonoBehaviour
    {
        private EventMaster eventMaster;
        [SerializeField] private int maxResultPlayerCount = 10;

        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform rowsParent;

        [SerializeField] private GameObject leaderboardPanel;

        private void OnEnable()
        {
            eventMaster.EventGameOver += SendToLeaderboard;
        }

        private void OnDisable()
        {
            eventMaster.EventGameOver -= SendToLeaderboard;
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        private void SendToLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new System.Collections.Generic.List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Highscore",
                    Value = score
                }
            }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Leaderboard Updated!");
        }

        public void DisplayLeaderboard() //Called by highscore button
        {
            leaderboardPanel.SetActive(true);

            var request = new GetLeaderboardRequest
            {
                StatisticName = "Highscore",
                StartPosition = 0,
                MaxResultsCount = maxResultPlayerCount
            };

            PlayFabClientAPI.GetLeaderboard(request, OnDisplayLeaderboard, OnError);
        }

        private void OnDisplayLeaderboard(GetLeaderboardResult result)
        {
            foreach (Transform item in rowsParent)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in result.Leaderboard)
            {
                AssignToMenuUI(item);
            }
        }

        private void AssignToMenuUI(PlayerLeaderboardEntry item)
        {
            GameObject go = Instantiate(rowPrefab, rowsParent);
            Text[] texts = go.GetComponentsInChildren<Text>();

            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }
    }
}