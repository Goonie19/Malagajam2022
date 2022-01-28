using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int finalScore;
    private int _score = 0;

    public void AddScore(int amount)
    {
        _score += amount;
    }

    public void ResetScore()
    {
        finalScore = _score;
        _score = 0;
    }
}
