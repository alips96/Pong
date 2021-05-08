using UnityEngine;

public class PlayFabMaster : MonoBehaviour
{
    public delegate void UserEventHandler(string name);
    public event UserEventHandler EventUserLoggedIn;

    public delegate void ScoreEventHandler(int score);
    public event ScoreEventHandler EventGameOver;

    public void CallEventUserLoggedIn(string displayName)
    {
        EventUserLoggedIn.Invoke(displayName);
    }

    public void CallEventGameOver(int score)
    {
        EventGameOver.Invoke(score);
    }
}
