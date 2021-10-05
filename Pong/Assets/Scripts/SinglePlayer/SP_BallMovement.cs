using UnityEngine;
using Zenject;

namespace Pong.SP
{
    public class SP_BallMovement : MonoBehaviour
    {
        private Rigidbody2D myrb;

        [HideInInspector] public Vector3 lastVelocity;
        public float ballInitialSpeed = 5f;

        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float maxSpeed = 15f;

        [SerializeField] private float minPlayerSize = 0.15f;
        [SerializeField] private float playerShrinkVolume = 0.02f;

        private SP_BallMovementModel ballMovementLogicScript;

        private void Start()
        {
            SetInitialVelocity();
        }

        private void SetInitialVelocity()
        {
            myrb = GetComponent<Rigidbody2D>();
            myrb.velocity = new Vector2(ballInitialSpeed, Random.Range(-2f, 2f));
        }

        private void Update()
        {
            lastVelocity = myrb.velocity;
        }

        [Inject]
        private void SetInitialReferences(SP_BallMovementModel _ballMovementModel)
        {
            ballMovementLogicScript = _ballMovementModel;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D contactPoint = collision.contacts[0];

            Vector3 myDirection = Vector3.Reflect(lastVelocity.normalized, contactPoint.normal);

            if (collision.transform.CompareTag("Player")) //if the ball hit the player.
            {
                //Shrink the player size
                contactPoint.collider.transform.localScale = 
                    ballMovementLogicScript.ShrinkPlayerSize(contactPoint.collider.transform.localScale,
                    minPlayerSize, playerShrinkVolume);

                //reflexion angle
                myDirection = ballMovementLogicScript.GetReflexionAngle(contactPoint, myDirection);
            }

            myrb.velocity = ballMovementLogicScript.UpdateVelocity(lastVelocity.magnitude, myDirection, lastVelocity.x, acceleration, maxSpeed);
        }
    }
}