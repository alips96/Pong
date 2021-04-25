using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text opponentScoreText;
    [SerializeField] private Text playerScoreText;

    private byte playerScore, opponentScore = 0;

    private void OnTriggerEnter2D(Collider2D bonusBar)
    {
        if (bonusBar.CompareTag("OppBar")) //Player Scored!
        {
            playerScore++;
            playerScoreText.text = playerScore.ToString();

        }
        else
        {
            opponentScore++;
            opponentScoreText.text = opponentScore.ToString();
        }
    }
}
