using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    public GameObject pausePanel;
    public InputManager _playerAction;

    private void Start()
    {
        _playerAction = InputManager.Instance;
    }

    void Update()
    {
        if (_playerAction.PlayerControls.UI.Pause.triggered)
        {
            if (!gameIsPaused)
                PauseGame();
            else
                ResumeGame();
        }
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
