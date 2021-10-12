using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.MP
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text opponentScoreText;
        [SerializeField] private Text playerScoreText;
        [SerializeField] private byte winScore = 10;

        private IScoreHandler_MP scoreHandler;

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += IncrementScore;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= IncrementScore;
        }

        [Inject]
        private void SetInitialReferences(IScoreHandler_MP _scoreHandler)
        {
            scoreHandler = _scoreHandler;
        }

        private void IncrementScore(EventData obj)
        {
            if (obj.Code == 1) //if the ball hits the bonus bars
            {
                if (obj.CustomData.ToString().Equals("OppBar")) //Player Scored!
                {
                    playerScoreText.text = scoreHandler.PlayerScored(winScore).ToString();
                }
                else
                {
                    opponentScoreText.text = scoreHandler.OpponentScored(winScore).ToString();
                }
            }
        }
    }
}