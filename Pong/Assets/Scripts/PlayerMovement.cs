using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    private Rigidbody2D myrb;
    private Vector3 mousePos;
    private float xPos;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            enabled = false;
        }

        myrb = GetComponent<Rigidbody2D>();
        xPos = transform.position.x;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        myrb.MovePosition(new Vector2(xPos, mousePos.y));
    }
}
