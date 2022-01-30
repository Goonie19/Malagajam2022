using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    public GameObject pausePanel;

    void Update()
    {
        if (!gameIsPaused && Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
        else if(gameIsPaused && Input.GetKeyDown(KeyCode.Escape))
            ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
}
