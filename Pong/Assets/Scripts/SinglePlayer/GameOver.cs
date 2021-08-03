using UnityEngine;
using Pong.General;
using Pong.MP.PlayFab;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    
    private PlayerMovement playerMovement;
    private EventMaster playFabMaster;

    private void OnEnable()
    {
        SetInitialReferences();

        playFabMaster.EventGameOver += PerformGameOverAction;
    }

    private void OnDisable()
    {
        playFabMaster.EventGameOver -= PerformGameOverAction;
    }

    private void PerformGameOverAction(int score)
    {
        gameOverPanel.SetActive(true);

        ResetBallPosition();

        playerMovement.enabled = false;
    }

    private void ResetBallPosition()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }

    private void SetInitialReferences()
    {
        playFabMaster = GameObject.Find("Network Manager").GetComponent<EventMaster>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
}
