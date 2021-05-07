using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SP_Leaderboard : MonoBehaviour
{
    private PlayFabMaster playFabMaster;
    [SerializeField] private int maxResultPlayerCount = 10;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventGameOver += SendToLeaderboard;
    }

    private void OnDisable()
    {
        playFabMaster.EventGameOver -= SendToLeaderboard;
    }

    private void SetInitialReferences()
    {
        playFabMaster = GetComponent<PlayFabMaster>();
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
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }
    }
}
