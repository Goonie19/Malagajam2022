using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState{ InMenu, Playing}

    private GameState _state = GameState.InMenu; 

    public Action OnGameEnded;

    public PlayerController PlayerReference;
    public GameObject MagneticObject;
    public Transform SpawnMagneticObject;

    public GameObject PlayerPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _state = GameState.InMenu;
    }

    public void Play()
    {
        PlayerReference.enabled = true;
        Instantiate(MagneticObject, SpawnMagneticObject.position, MagneticObject.transform.rotation);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadSceneInt(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
