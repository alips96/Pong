using NUnit.Framework;
using UnityEngine;
using Pong.SP;

namespace Tests
{
    public class BallMovementTest
    {
        [Test]
        [TestCase(0.2f, 1f, .7f, 5)]
        [TestCase(.6f, .4f, .3f, 11)]
        public void TestIncrementSpeed(float dx, float dy, float dz, float speed)
        {
            GameObject go = new GameObject();
            SP_BallMovement ballMovementScript = go.AddComponent<SP_BallMovement>();

            Vector2 velocity = ballMovementScript.UpdateVelocity(speed, new Vector3(dx, dy, dz));

            Assert.AreEqual(speed * new Vector2(dx, dy), velocity);
        }
    }
}