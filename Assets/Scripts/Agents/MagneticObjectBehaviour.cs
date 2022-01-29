using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectBehaviour : MonoBehaviour
{
    public float MaxMagneticForce = 4f;

    public float WallMagneticForce = 10f;

    public bool PositivePoleUp
    {
        get => _positivePoleUp;
        set => _positivePoleUp = value;
    }

    private bool _positivePoleUp = true;

    private Vector2 _magneticDirection;
    private float _actualMagneticForce;

    private bool _playerCanRepel;

    private Vector2 _wallMagneticDirection;

    private Rigidbody2D _rb;
    private Collider2D _col;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, _magneticDirection * MaxMagneticForce));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(transform.position, _wallMagneticDirection));
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = gameObject.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {

        if (!_playerCanRepel)
            _magneticDirection = Vector2.zero;

        _rb.AddForce((_magneticDirection * _actualMagneticForce) + (_wallMagneticDirection * WallMagneticForce));
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirections();

        CheckForces();

    }

    public void Rotate()
    {
        GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;

        _positivePoleUp = !_positivePoleUp;
    }

    private void CheckForces()
    {
        _actualMagneticForce = Mathf.Clamp(Vector2.Distance(GameManager.Instance.PlayerReference.transform.position, transform.position), 0, 1) * MaxMagneticForce;
    }

    private void CheckDirections()
    {
        if(GameManager.Instance.PlayerReference.transform.position.y >= transform.position.y)
        {
            if (GameManager.Instance.PlayerReference.PositivePoleUp != _positivePoleUp)
                _magneticDirection = (GameManager.Instance.PlayerReference.transform.position - transform.position).normalized;
            else
                _magneticDirection = (GameManager.Instance.PlayerReference.transform.position - transform.position).normalized * -1f;
        }
        else
        {
            if (GameManager.Instance.PlayerReference.PositivePoleUp == _positivePoleUp)
                _magneticDirection = (GameManager.Instance.PlayerReference.transform.position - transform.position).normalized;
            else
                _magneticDirection = (GameManager.Instance.PlayerReference.transform.position - transform.position).normalized * -1f;
        }
    }

    private void SetChildOfTheMagnet(GameObject magnet)
    {
        gameObject.transform.SetParent(magnet.transform);
    }

    private void DesactivatePhysics()
    {
        Destroy(_rb);
        _col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _wallMagneticDirection += collision.GetComponent<WallMagneticBehaviour>().GetWallRepellDirection();
        }
        else if (collision.CompareTag("Player"))
            _playerCanRepel = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _wallMagneticDirection = Vector2.zero;
        }
        else if (collision.CompareTag("Player"))
            _playerCanRepel = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
            GameManager.Instance.StopGame();
        else if (collision.gameObject.CompareTag("Player")) {
            DesactivatePhysics();
            SetChildOfTheMagnet(collision.gameObject);
            if(UnityEngine.Random.Range(0,1) == 0)
                transform.position = GameManager.Instance.PlayerReference.StuckPos.position;
            else
                transform.position = GameManager.Instance.PlayerReference.StuckPos2.position;
            foreach (Transform g in GetComponentInChildren<Transform>())
                Destroy(g.gameObject);

            GameManager.Instance.SpawnAnotherBall();
            this.enabled = false;

        }
    }

    
}
