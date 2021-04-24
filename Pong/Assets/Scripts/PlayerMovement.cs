using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myrb;
    private Vector3 mousePos;

    private void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {
        myrb.MovePosition(new Vector2(7, mousePos.y));
    }
}
