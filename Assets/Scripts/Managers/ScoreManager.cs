using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static int Score = 0;

    public static void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        var val = UIManager._uiTextElements["scoreText"];
        UIManager.UpdateUITextElement(val, Score.ToString());
    }
}
