using UnityEngine;

public class PlayFabMaster : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventUserLoggedIn;

    public void CallEventUserLoggedIn()
    {
        EventUserLoggedIn.Invoke();
    }
}
