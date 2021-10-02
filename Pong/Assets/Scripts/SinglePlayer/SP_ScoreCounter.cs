using Pong.General;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.SP
{
    public class SP_ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        private int score;

        private EventMaster eventMaster;

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                score++;
                scoreText.text = score.ToString();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) //GameOver
        {
            eventMaster.CallEventGameOver(score);
            score = 0;
        }
    }
}