using UnityEngine;
using UnityEngine.UI;

public class SP_RestartLevel : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Text scoreText;

    [SerializeField] private float ballInitialSpeed = 5f;

    public void ResumeGame()
    {
        playerMovement.enabled = true;
        scoreText.text = "0";
        GetComponent<Rigidbody2D>().velocity = new Vector2(ballInitialSpeed, Random.Range(-2f, 2f));
    }
}
