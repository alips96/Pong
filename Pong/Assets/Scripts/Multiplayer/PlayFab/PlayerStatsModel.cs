using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.MP.PlayFab
{
    public class PlayerStatsModel
    {
        public string CalculateWinPercentage(int winCount, int matchCount)
        {
            float percentage = winCount * 100f / matchCount;
            return ((float)Math.Round(percentage * 10f) / 10f).ToString(); //round this into one decimal point.
        }
    }
}
