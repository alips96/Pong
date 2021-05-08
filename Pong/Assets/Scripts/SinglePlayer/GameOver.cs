using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    
    private PlayerMovement playerMovement;
    private PlayFabMaster playFabMaster;

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
        playFabMaster = GameObject.Find("NetworkManager").GetComponent<PlayFabMaster>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
}
