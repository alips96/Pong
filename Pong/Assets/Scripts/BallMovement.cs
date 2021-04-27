using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D myrb;
    private Vector3 lastVelocity;

    [SerializeField] private float ballInitialSpeed = 5f;
    [SerializeField] private int setPointInterval = 2;
    [SerializeField] private float acceleration = 0.1f;

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
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        myrb.velocity = direction * Mathf.Max(speed, 0);
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
