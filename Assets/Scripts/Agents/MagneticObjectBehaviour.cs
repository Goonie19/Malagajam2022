using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectBehaviour : MonoBehaviour
{
    public float MaxMagneticForce = 4f;

    public float WallMagneticForce = 10f;

    private bool _positivePoleUp = true;

    private Vector2 _magneticDirection;
    private float _actualMagneticForce;

    private Vector2 _wallMagneticDirection;

    private Rigidbody2D _rb;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        _rb.AddForce((_magneticDirection * _actualMagneticForce) + (_wallMagneticDirection * WallMagneticForce));
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirections();

        CheckForces();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            _wallMagneticDirection += collision.GetComponent<WallMagneticBehaviour>().GetWallRepellDirection();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _wallMagneticDirection = Vector2.zero;
        }
    }


}
