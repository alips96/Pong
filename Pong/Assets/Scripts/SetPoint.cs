using System.Collections;
using UnityEngine;

public class SetPoint : MonoBehaviour
{
    [SerializeField] private float ballInitialSpeed;
    [SerializeField] private int setPointInterval;

    private void OnTriggerEnter2D(Collider2D other)
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
