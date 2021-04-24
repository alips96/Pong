using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody2D myrb;
    private Vector2 velocity = new Vector2(8, 0);
    private Vector3 lastVelocity;

    private void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        myrb.velocity = velocity;
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
}
