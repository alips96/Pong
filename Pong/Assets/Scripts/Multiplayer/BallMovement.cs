using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Pong.MP
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody2D myrb;
        private Vector3 lastVelocity;

        [SerializeField] private float ballInitialSpeed = 5f;
        [SerializeField] private int setPointInterval = 2;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float maxSpeed = 15f;

        [SerializeField] private float minPlayerSize = 0.17f;
        [SerializeField] private float playershrinkVolume = 0.01f;

        private bool playersListEmpty = true;
        private GameObject[] playersObjects;

        private float angleOffset = 0.5f;

        private void OnEnable()
        {
            myrb = GetComponent<Rigidbody2D>();

            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(WaitForTheClientToJoin());
                SendInitialVelocityToClients();
            }

            PhotonNetwork.NetworkingClient.EventReceived += SetVelocity;
            PhotonNetwork.NetworkingClient.EventReceived += ResetPosAndSpeed;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= SetVelocity;
            PhotonNetwork.NetworkingClient.EventReceived -= ResetPosAndSpeed;
        }

        private void SetVelocity(EventData obj)
        {
            if (obj.Code == 0)
            {
                float[] args = (float[])obj.CustomData; // [0]: random number, [1]: 1 for positive direction and -1 for the negative one.
                myrb.velocity = new Vector2(ballInitialSpeed * args[1], args[0]);
            }
        }

        private void SendInitialVelocityToClients()
        {
            float chosenRandomAngle = ChooseXAngle();

            float[] eventArgs = new float[2] { chosenRandomAngle, 1 }; // first argument is the velocity. second one determines the direction.

            PhotonNetwork.RaiseEvent(0, eventArgs,
                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                new SendOptions { Reliability = true });
        }

        private float ChooseXAngle()
        {
            float randomAngle = Random.Range(-2f, 2f);

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

        IEnumerator WaitForTheClientToJoin()
        {
            yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == 2);
        }

        private void Update()
        {
            lastVelocity = myrb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float speed = lastVelocity.magnitude;
            ContactPoint2D contactPoint = collision.contacts[0];

            Vector3 myDirection = Vector3.Reflect(lastVelocity.normalized, contactPoint.normal);
            Vector2 myVelocity = myDirection * Mathf.Max(speed, 0);

            if (lastVelocity.x * myDirection.x < 0) //if ball hits the players, it toggles direction :)
            {
                //Shrink the players size
                if (playersListEmpty)
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

                myVelocity += myDirection.x * new Vector2(acceleration, 0);

                if (Mathf.Abs(myVelocity.x) > maxSpeed) //if it reaches max speed.
                {
                    myVelocity = new Vector2(maxSpeed * myDirection.x, myVelocity.y);
                }
            }

            myrb.velocity = myVelocity;
        }

        private void OnTriggerEnter2D(Collider2D other) //The ball hits the bonus bars
        {
            if (other.CompareTag("OppBar"))
            {
                if (!PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.RaiseEvent(1, other.tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
                }
                else
                {
                    PhotonNetwork.RaiseEvent(3, other.tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
                }
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.RaiseEvent(1, other.tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
                }
                else
                {
                    PhotonNetwork.RaiseEvent(3, other.tag, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
                }
            }
        }

        private void ResetPosAndSpeed(EventData obj)
        {
            if (obj.Code == 1 || obj.Code == 3)
            {
                ResetMatchSettings();
                StartCoroutine(RestartSetPoint(setPointInterval, obj.CustomData.ToString()));
            }
        }

        private void ResetMatchSettings()
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = Vector3.zero;

            if (playersObjects.Length.Equals(2))
            {
                foreach (var item in playersObjects)
                {
                    item.transform.localScale = new Vector3(0.02f, 0.5f, 1f);
                }
            }
            else
            {
                Debug.LogError("Player list length is less than 2");
            }
        }

        private IEnumerator RestartSetPoint(int seconds, string tag)
        {
            yield return new WaitForSeconds(seconds);

            if (PhotonNetwork.IsMasterClient)
            {
                float randomNumber = ChooseXAngle();

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