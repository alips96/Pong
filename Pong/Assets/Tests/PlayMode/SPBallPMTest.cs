using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pong.SP;

namespace Tests
{
    public class SPBallPMTest
    {
        [UnityTest]
        public IEnumerator SPBallPMTestWithEnumeratorPasses()
        {
            GameObject go = new GameObject();
            go.AddComponent<Rigidbody2D>();
            SP_BallMovement ballMovementScript = go.AddComponent<SP_BallMovement>();

            yield return null;

            Assert.AreEqual(ballMovementScript.ballInitialSpeed, (int)ballMovementScript.lastVelocity.magnitude);
;        }
    }
}