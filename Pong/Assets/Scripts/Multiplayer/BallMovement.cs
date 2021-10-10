using System.Collections;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Zenject;

namespace Pong.MP
{
    public class BallMovement : MonoBehaviour
    {
        private Rigidbody2D myrb;
        private Vector3 lastVelocity;

        [SerializeField] private int setPointInterval = 2;

        public float acceleration = 0.3f;
        public float maxSpeed = 15f;

        private IMoveBall moveBallBehavior;

        private void OnEnable()
        {
            FillInitialComponents();

            SyncrinizeSceneToClients();

            PhotonNetwork.NetworkingClient.EventReceived += SetVelocity;
            PhotonNetwork.NetworkingClient.EventReceived += ResetPosAndSpeed;
        }

        private void FillInitialComponents()
        {
            myrb = GetComponent<Rigidbody2D>();
        }

        private void SyncrinizeSceneToClients()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(WaitForTheClientToJoin());
                moveBallBehavior.SendInitialVelocityToClients();
            }
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= SetVelocity;
            PhotonNetwork.NetworkingClient.EventReceived -= ResetPosAndSpeed;
        }

        [Inject]
        private void SetInitialReferences(IMoveBall moveBall)
        {
            moveBallBehavior = moveBall;
        }

        private void SetVelocity(EventData obj)
        {
            if (obj.Code == 0)
            {
                myrb.velocity = moveBallBehavior.SetVelocity((float[])obj.CustomData);
            }
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

            myrb.velocity = moveBallBehavior.ReflectBall(speed, contactPoint, lastVelocity);
        }

        private void OnTriggerEnter2D(Collider2D other) //The ball hits the bonus bars
        {
            if (other.CompareTag("OppBar"))
            {
                moveBallBehavior.OpponentScored(other.tag);
            }
            else
            {
                moveBallBehavior.PlayerScored(other.tag);
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

            moveBallBehavior.ResetPlayersScale();
        }

        private IEnumerator RestartSetPoint(int seconds, string tag)
        {
            yield return new WaitForSeconds(seconds);

            moveBallBehavior.RestartSetPoint(tag);
        }
    }
}