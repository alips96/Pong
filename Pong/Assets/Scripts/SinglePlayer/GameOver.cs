using UnityEngine;
using Pong.General;
using Zenject;

namespace Pong.SP
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private PlayerMovement playerMovement;
        private EventMaster eventMaster;

        private void OnEnable()
        {
            SetPlayerMovementReference();

            eventMaster.EventGameOver += PerformGameOverAction;
        }

        private void OnDisable()
        {
            eventMaster.EventGameOver -= PerformGameOverAction;
        }

        private void PerformGameOverAction(int score)
        {
            gameOverPanel.SetActive(true);

            ResetBallPosition();
            ResetPlayerSize();

            playerMovement.enabled = false;
        }

        private void ResetPlayerSize()
        {
            playerMovement.transform.localScale = new Vector3(0.02f, 0.5f, 1);
        }

        private void ResetBallPosition()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = Vector3.zero;
        }

        private void SetPlayerMovementReference()
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }
    }
}