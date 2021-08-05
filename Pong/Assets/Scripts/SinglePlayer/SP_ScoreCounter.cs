using Pong.General;
using UnityEngine;
using UnityEngine.UI;

namespace Pong.SP
{
    public class SP_ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        private int score;
        private EventMaster playfabMasterScript;

        private void Start()
        {
            playfabMasterScript = GameObject.Find("Network Manager").GetComponent<EventMaster>();
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
            playfabMasterScript.CallEventGameOver(score);
            score = 0;
        }
    }
}