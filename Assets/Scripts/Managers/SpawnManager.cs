using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> objectsToSpawn;
    public List<Transform> spawnPositions;

    [Header("Speed")]
    public float moveSpeed;

    [Header("Edges")]
    public float X_Edge;

    [Header("Timers")]
    public float waitingTime; //Tiempo que espera antes de comenzar a spawnear
    public float timeBetweenSpawns; //Tiempo entre spawns

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
            MoveSpawner();
            _timer += Time.deltaTime;
            if(_timer >= timeBetweenSpawns)
            {
                SpawnObject();
                _timer = 0f;
            }
        }
    }

    private void MoveSpawner()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if(transform.position.x > X_Edge || transform.position.x < -X_Edge)
        {
            Debug.Log("cambio de sentido");
            moveSpeed *= -1;
        }
    }

    #region SPAWN METHODS

    public void SpawnObject()
    {
        GetRandomObject();
        //GetRandomPosition();
        Instantiate(_obj, transform.position, Quaternion.identity);
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
