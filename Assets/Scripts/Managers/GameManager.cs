using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnGameEnded;

    public Vector2 TimeRange;

    public PlayerController PlayerReference;
    public List<GameObject> MagneticObject;
    public Transform SpawnMagneticObject;

    public GameObject PlayerPrefab;

    public FMODUnity.EventReference gameOverSound;

    private GameObject _magneticObject;

    Coroutine _coroutineForFlip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        
    }

    public void Play()
    {
        if (_coroutineForFlip != null)
            StopCoroutine(_coroutineForFlip);
        PlayerReference.enabled = true;
        SpawnAnotherBall();

        _coroutineForFlip = StartCoroutine(RotateMagnetOBJ());
    }

    public void SpawnAnotherBall()
    {
        if (_magneticObject && _magneticObject.transform.parent != PlayerReference.transform) return;

        int r = UnityEngine.Random.Range(0, MagneticObject.Count);
        _magneticObject = Instantiate(MagneticObject[r], SpawnMagneticObject.position, MagneticObject[r].transform.rotation);
    }

    IEnumerator RotateMagnetOBJ()
    {
        while(true)
        {
            float r = UnityEngine.Random.Range(TimeRange.x, TimeRange.y);

            yield return new WaitForSeconds(r);

            _magneticObject.GetComponent<MagneticObjectBehaviour>().Rotate();
        }
    }

    public void StopGame()
    {
        ScoreManager.Instance.ResetScore();
        FMODUnity.RuntimeManager.PlayOneShot(gameOverSound);
        PlayerReference.gameObject.SetActive(false);
        _magneticObject.SetActive(false);
        StopCoroutine(_coroutineForFlip);

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
