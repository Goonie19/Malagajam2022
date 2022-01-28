using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> objectsToSpawn;
    public List<Transform> spawnPositions;

    public float waitingTime; //Tiempo que espera antes de comenzar a spawnear
    public float timeBetweenSpawns; //Tiempo entre spawns
    public GameObject _objSpawn; //Variable auxiliar

    private GameObject _obj;
    private Transform _pos;
    private bool _startSpawning = false;
    private float _timer = 0f;

    private void Start()
    {
        Invoke(nameof(WaitToStart), waitingTime);
    }

    private void Update()
    {
        if (_startSpawning)
        {
            _timer += Time.deltaTime;
            if(_timer >= timeBetweenSpawns)
            {
                SpawnObject();
                _timer = 0f;
            }
        }
    }

    #region SPAWN METHODS

    public void SpawnObject()
    {
        GetRandomObject();
        GetRandomPosition();
        Instantiate(_obj, _pos.position, Quaternion.identity);
    }

    private void GetRandomObject()
    {
        _obj = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
    }

    private void GetRandomPosition()
    {
        _pos = spawnPositions[Random.Range(0, spawnPositions.Count)];
    }
    #endregion


    private void WaitToStart()
    {
        _startSpawning = true;
    }

    public void StopSpawn()
    {
        _startSpawning = false;
    }

}
