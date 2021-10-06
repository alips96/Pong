using NUnit.Framework;
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
            PlayerStatsModel playerStats = new PlayerStatsModel();

            string count = playerStats.CalculateWinPercentage(winCount, matchCount);

            Assert.AreEqual(count, expected);
        }
    }
}
