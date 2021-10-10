using UnityEngine;

namespace Pong.MP
{
    public interface IMoveBall
    {
        void SendInitialVelocityToClients();

        Vector2 ReflectBall(float currentSpeed, ContactPoint2D contactPoint, Vector3 currentVelocity);

        void PlayerScored(string tag);
        void OpponentScored(string tag);

        void ResetPlayersScale();
        void RestartSetPoint(string tag);
        Vector2 SetVelocity(float[] args);
    }
}