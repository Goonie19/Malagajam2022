using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int finalScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    public GameObject finishPanel;

    private int _score = 0;

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreOnCanvas();
    }

    private void UpdateScoreOnCanvas()
    {
        scoreText.text = _score.ToString();
    }

    private void ShowFinalScore()
    {
        finalScoreText.text = finalScore.ToString();
        finishPanel.SetActive(true);
    }

    public void ResetScore()
    {
        finalScore = _score;
        ShowFinalScore();
        _score = 0;
    }
}
