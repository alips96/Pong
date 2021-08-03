using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

namespace Pong.MP
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text opponentScoreText;
        [SerializeField] private Text playerScoreText;
        [SerializeField] private byte winScore = 10;

        private byte playerScore, opponentScore = 0;

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += IncrementScore;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= IncrementScore;
        }

        private void IncrementScore(EventData obj)
        {
            if (obj.Code == 1) //if the ball hits the bonus bars
            {
                if (obj.CustomData.ToString().Equals("OppBar")) //Player Scored!
                {
                    playerScoreText.text = (++playerScore).ToString();

                    if (playerScore == winScore) //master player won
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonNetwork.RaiseEvent(2, true,
                                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                                new SendOptions { Reliability = true });
                        }
                    }
                }
                else
                {
                    opponentScoreText.text = (++opponentScore).ToString();

                    if (opponentScore == winScore) //second player won
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonNetwork.RaiseEvent(2, false,
                                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                                new SendOptions { Reliability = true });
                        }
                    }
                }
            }
        }
    }
}