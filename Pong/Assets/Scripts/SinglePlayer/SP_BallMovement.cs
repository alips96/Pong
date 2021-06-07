using System.Collections;
using UnityEngine;

public class SP_BallMovement : MonoBehaviour
{
    private Rigidbody2D myrb;
    private Vector3 lastVelocity;

    [SerializeField] private float ballInitialSpeed = 5f;
    [SerializeField] private float acceleration = 0.3f;
    [SerializeField] private float maxSpeed = 15f;

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

        Vector3 myDirection = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        if (collision.transform.tag == "Player")
        {
            float difference = collision.contacts[0].point.y - collision.contacts[0].collider.transform.position.y;
            float clampedDifference = Mathf.Clamp(difference, -0.5f, 0.5f);
            myDirection = new Vector3(myDirection.x, clampedDifference, 0);
        }

        Vector2 myVelocity = myDirection * Mathf.Max(speed, 0);    

        if (lastVelocity.x * myDirection.x < 0) //if ball hits the players, because it toggles direction :)
        {
            myVelocity += myDirection.normalized.x * new Vector2(acceleration, 0);

            if (Mathf.Abs(myVelocity.x) > maxSpeed) //if it reaches max speed.
            {
                myVelocity = new Vector2(maxSpeed * myDirection.normalized.x, myVelocity.y);
            }
        }

        myrb.velocity = myVelocity;
    }
}
