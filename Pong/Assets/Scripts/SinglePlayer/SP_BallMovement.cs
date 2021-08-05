using UnityEngine;

namespace Pong.SP
{
    public class SP_BallMovement : MonoBehaviour
    {
        private Rigidbody2D myrb;
        private Vector3 lastVelocity;

        [SerializeField] private float ballInitialSpeed = 5f;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float maxSpeed = 15f;

        [SerializeField] private float minPlayerSize = 0.15f;
        [SerializeField] private float playershrinkVolume = 0.02f;

        private void Start()
        {
            myrb = GetComponent<Rigidbody2D>();
            myrb.velocity = new Vector2(ballInitialSpeed, Random.Range(-2f, 2f));
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

            if (collision.transform.CompareTag("Player"))
            {
                //Shrink the player size
                Vector3 playerLocalScale = contactPoint.collider.transform.localScale;

                if (playerLocalScale.y >= minPlayerSize)
                {
                    contactPoint.collider.transform.localScale = new Vector3(playerLocalScale.x, playerLocalScale.y - playershrinkVolume, 0);
                }

                //reflexion angle
                float difference = contactPoint.point.y - contactPoint.collider.transform.position.y;
                float clampedDifference = Mathf.Clamp(difference, -0.5f, 0.5f);
                myDirection = new Vector3(myDirection.x, clampedDifference, 0);
            }

            myrb.velocity = UpdateVelocity(speed, myDirection);
        }

        public Vector2 UpdateVelocity(float speed, Vector3 myDirection)
        {
            Vector2 myVelocity = myDirection * Mathf.Max(speed, 0);

            if (lastVelocity.x * myDirection.x < 0) //if ball hits the players, it toggles direction :)
            {
                myVelocity += myDirection.x * new Vector2(acceleration, 0);

                if (Mathf.Abs(myVelocity.x) > maxSpeed) //if it reaches max speed.
                {
                    myVelocity = new Vector2(maxSpeed * myDirection.x, myVelocity.y);
                }
            }

            return myVelocity;
        }
    }
}
