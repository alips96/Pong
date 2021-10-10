using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Pong.MP
{
    public class BallMovementModel : IMoveBall
    {
        private bool playersListEmpty = true;
        private GameObject[] playersObjects;
        private Vector3 initialPlayerScale = new Vector3(0.02f, 0.5f, 1f);

        private float ballInitialSpeed = 5f;
        private float minPlayerSize = 0.17f;
        private float playershrinkVolume = 0.01f;
        private float angleOffset = 0.5f;

        public float acceleration = 0.3f;
        public float maxSpeed = 15f;

        public void SendInitialVelocityToClients()
        {
            float chosenRandomAngle = ChooseXAngle(Random.Range(-2f, 2f));

            float[] eventArgs = new float[2] { chosenRandomAngle, 1 }; // first argument is the velocity. second one determines the direction.

            PhotonNetwork.RaiseEvent(0, eventArgs,
                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                new SendOptions { Reliability = true });
        }

        public float ChooseXAngle(float randomAngle)
        {
            if (randomAngle > -.2f && randomAngle < .2f)
            {
                if (randomAngle.Equals(0))
                {
                    randomAngle = angleOffset; //shift 0.5
                }
                else
                {
                    randomAngle += randomAngle / Mathf.Abs(randomAngle) * angleOffset;
                }
            }

            return randomAngle;
        }

        public Vector2 SetVelocity(float[] args)
        {
            // args:: [0]: random number, [1]: 1 for positive direction and -1 for the negative one.
            return new Vector2(ballInitialSpeed * args[1], args[0]);
        }

        public Vector2 ReflectBall(float speed, ContactPoint2D contactPoint, Vector3 lastVelocity)
        {
            Vector3 myDirection = Vector3.Reflect(lastVelocity.normalized, contactPoint.normal);
            Vector2 myVelocity = myDirection * Mathf.Max(speed, 0);

            if (lastVelocity.x * myDirection.x < 0) //if ball hits the players, it toggles direction :)
            {
                //Shrink the players size
                ShrinkPlayersSize();

                myVelocity = UpdateVelocity(myDirection, myVelocity);
            }

            return myVelocity;
        }

        public Vector2 UpdateVelocity(Vector3 myDirection, Vector2 myVelocity)
        {
            myVelocity += myDirection.x * new Vector2(acceleration, 0);

            if (Mathf.Abs(myVelocity.x) > maxSpeed) //if it reaches max speed.
            {
                myVelocity = new Vector2(maxSpeed * myDirection.x, myVelocity.y);
            }

            return myVelocity;
        }

        private void ShrinkPlayersSize()
        {
            if (playersListEmpty) //Getting all players
            {
                playersObjects = GameObject.FindGameObjectsWithTag("Player");

                if (playersObjects.Length.Equals(2))
                    playersListEmpty = false;
            }

            foreach (GameObject item in playersObjects)
            {
                Vector3 playerLocalScale = item.transform.localScale;

                if (playerLocalScale.y >= minPlayerSize)
                {
                    item.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y - playershrinkVolume, 0);
                }
            }
        }

        public void PlayerScored(string tag)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.RaiseEvent(1, tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
            }
            else
            {
                PhotonNetwork.RaiseEvent(3, tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
            }
        }

        public void OpponentScored(string tag)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.RaiseEvent(1, tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
            }
            else
            {
                PhotonNetwork.RaiseEvent(3, tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
            }
        }

        public void ResetPlayersScale()
        {
            if (playersObjects.Length.Equals(2))
            {
                foreach (var item in playersObjects)
                {
                    item.transform.localScale = initialPlayerScale;
                }
            }
            else
            {
                Debug.LogError("Player list length is less than 2");
            }
        }

        public void RestartSetPoint(string tag)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                float randomNumber = ChooseXAngle(Random.Range(-2f, 2f));

                if (tag.Equals("OppBar"))
                {
                    float[] eventArgs = new float[2] { randomNumber, 1f }; // first argument is the velocity. Second one determines the direction.

                    PhotonNetwork.RaiseEvent(0, eventArgs,
                        new RaiseEventOptions { Receivers = ReceiverGroup.All },
                        new SendOptions { Reliability = true });
                }
                else
                {
                    float[] eventArgs = new float[2] { randomNumber, -1f }; // first argument is the velocity. second one determines the direction.

                    PhotonNetwork.RaiseEvent(0, eventArgs,
                        new RaiseEventOptions { Receivers = ReceiverGroup.All },
                        new SendOptions { Reliability = true });
                }
            }
        }
    }
}