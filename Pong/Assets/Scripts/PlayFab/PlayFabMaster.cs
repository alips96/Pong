using UnityEngine;

public class PlayFabMaster : MonoBehaviour
{
    public delegate void UserEventHandler(string name);
    public event UserEventHandler EventUserLoggedIn;

    public void CallEventUserLoggedIn(string displayName)
    {
        EventUserLoggedIn.Invoke(displayName);
    }
}
