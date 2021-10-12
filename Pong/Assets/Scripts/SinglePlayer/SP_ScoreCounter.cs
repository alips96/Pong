using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.SP
{
    public class SP_ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        private IScoreHandler_SP scoreHandler;

        [Inject]
        private void SetInitialReferences(IScoreHandler_SP someScoreHandler)
        {
            scoreHandler = someScoreHandler;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                int score = scoreHandler.AddPoint();
                scoreText.text = score.ToString();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) //GameOver
        {
            scoreHandler.PerformGameOver();
        }
    }
}