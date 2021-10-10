using NUnit.Framework;
using UnityEngine;
using Pong.SP;
using Pong.MP;

namespace Tests
{
    public class BallMovementTest
    {
        private SP_BallMovementModel ballMovementLogic;

        public BallMovementTest()
        {
            ballMovementLogic = new SP_BallMovementModel();
        }

        [Test]
        [TestCase(0.2f, 1f, .7f, 5)]
        [TestCase(.6f, .4f, .3f, 11)]
        public void TestIncrementSpeed(float dx, float dy, float dz, float speed)
        {
            Vector2 velocity = ballMovementLogic.UpdateVelocity(speed, new Vector3(dx, dy, dz), speed, .3f, 15f);

            Assert.AreEqual(speed * new Vector2(dx, dy), velocity);
        }

        [Test]
        [TestCase(0, .5f)]
        [TestCase(.2f, .2f)]
        [TestCase(-.1f, -.6f)]
        [TestCase(.05f, .55f)]
        [TestCase(-.02f, -.52f)]
        public void TestInitialVelocity(float randomInput, float expected)
        {
            BallMovementModel moveBallScript = new BallMovementModel();

            float angle = moveBallScript.ChooseXAngle(randomInput);

            Assert.AreEqual(expected, angle);
        }

        [Test]
        [TestCase(1, 5f, 5.3f)]
        [TestCase(-1, -5.9f, -6.2f)]
        [TestCase(-1, -20f, -15f)]
        public void TestSubsequentVelocity(float dx, float vx, float evx)
        {
            BallMovementModel moveBallScript = new BallMovementModel();

            Vector2 velocity = moveBallScript.UpdateVelocity(new Vector3(dx, -.1f, 0), new Vector2(vx, -.6f));

            if (Mathf.Abs(vx) > moveBallScript.maxSpeed)
            {
                Assert.AreEqual(dx * moveBallScript.maxSpeed, velocity.x);
            }
            else
            {
                Assert.AreEqual(evx, Mathf.Round(velocity.x * 100f) / 100f); //Need to round cause it might have minor differences like 0.0001
            }
        }
    }
}