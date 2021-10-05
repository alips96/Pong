using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_BallMovementModel
{
    public Vector3 ShrinkPlayerSize(Vector3 playerLocalScale, float minPlayerSize, float playerShrinkVolume)
    {
        if (playerLocalScale.y >= minPlayerSize)
        {
            return new Vector3(playerLocalScale.x, playerLocalScale.y - playerShrinkVolume, 0);
        }

        return playerLocalScale;
    }

    public Vector3 GetReflexionAngle(ContactPoint2D contactPoint, Vector3 myDirection)
    {
        float difference = contactPoint.point.y - contactPoint.collider.transform.position.y;
        float clampedDifference = Mathf.Clamp(difference, -0.5f, 0.5f);

        return new Vector3(myDirection.x, clampedDifference, 0);
    }

    public Vector2 UpdateVelocity(float speed, Vector3 myDirection, float lastVelocity, float acceleration, float maxSpeed)
    {
        Vector2 myVelocity = myDirection * Mathf.Max(speed, 0);

        if (lastVelocity * myDirection.x < 0) //if ball hits the players, it toggles direction :)
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
