using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pong.MP.PlayFab;

namespace Tests
{
    public class StatsTest
    {
        [Test]
        [TestCase(2,2,"100")]
        [TestCase(1,2,"50")]
        [TestCase(7,12,"58.3")]
        public void WinPercentageCalculation(int winCount, int matchCount, string expected)
        {
            GameObject go = new GameObject();
            PlayerStatsUI playerStats = go.AddComponent<PlayerStatsUI>();

            string count = playerStats.CalculateWinPercentage(winCount, matchCount);

            Assert.AreEqual(count, expected);
        }
    }
}
