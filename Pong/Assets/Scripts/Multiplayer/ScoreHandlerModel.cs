using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Pong.MP
{
    public class ScoreHandlerModel : IScoreHandler_MP
    {
        private byte playerScore = 0, opponentScore = 0;

        public byte OpponentScored(int winScore)
        {
            opponentScore++;

            if (opponentScore == winScore) //second player won
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.RaiseEvent(2, false,
                        new RaiseEventOptions { Receivers = ReceiverGroup.All },
                        new SendOptions { Reliability = true });
                }
            }

            return opponentScore;
        }

        public byte PlayerScored(int winScore)
        {
            playerScore++;

            if (playerScore == winScore) //master player won
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.RaiseEvent(2, true,
                        new RaiseEventOptions { Receivers = ReceiverGroup.All },
                        new SendOptions { Reliability = true });
                }
            }

            return playerScore;
        }
    }
}