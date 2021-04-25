using System.Collections;
using UnityEngine;

public class SetPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.zero;
        StartCoroutine(WaitForSeconds(2, other.tag));
    }

    private IEnumerator WaitForSeconds(int seconds, string tag)
    {
        yield return new WaitForSeconds(seconds);

        if (tag.Equals("OppBar"))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 1);
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 1);
        }    

    }
}
