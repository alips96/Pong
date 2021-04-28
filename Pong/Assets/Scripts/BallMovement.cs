using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D myrb;
    private Vector3 lastVelocity;

    [SerializeField] private float ballInitialSpeed = 5f;
    [SerializeField] private int setPointInterval = 2;
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

    private void OnTriggerEnter2D(Collider2D other) //The ball hits the bonus bars
    {
        ResetBallSpeedAndPosition();

        StartCoroutine(RestartSetPoint(setPointInterval, other.tag));
    }

    private void ResetBallSpeedAndPosition()
    {
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }

    private IEnumerator RestartSetPoint(int seconds, string tag)
    {
        yield return new WaitForSeconds(seconds);

        if (tag.Equals("OppBar"))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(ballInitialSpeed, Random.Range(-2f, 2f));
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-ballInitialSpeed, Random.Range(-2f, 2f));
        }

    }
}
