using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3f;

    public Animator Anim;

    public Transform StuckPos;
    public Transform StuckPos2;

    public bool PositivePoleUp
    {
        get => _positivePoleUp;
    }

    private Rigidbody2D _rb;

    private bool _positivePoleUp;

    private float _moveDirection;
    private float _actualSpeed;

    private void Awake()
    {
        _positivePoleUp = true;
        _rb = GetComponent<Rigidbody2D>();
        _actualSpeed = Speed;
        GameManager.Instance.PlayerReference = this;
    }

    private void Start()
    {
        InputManager.Instance.PlayerControls.Player.Rotate.performed += ctx =>
        {
            Rotate();
        };
    }


    private void Update()
    {
        CheckInputs();
        CheckPosition();
    }

    private void CheckPosition()
    {
        Anim.SetFloat("PositionInCamera", Camera.main.WorldToScreenPoint(transform.position).x / Screen.width);
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.right * _actualSpeed * _moveDirection;
    }

    private void CheckInputs()
    {
        _moveDirection = InputManager.Instance.PlayerControls.Player.Movement.ReadValue<float>();

        
    }

    private void Rotate()
    {
        _positivePoleUp = !_positivePoleUp;
        Anim.SetTrigger("Rotate");
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MagneticObject") || collision.gameObject.CompareTag("Player"))
        {
            _actualSpeed *= 0.8f;
        } 
    }
}
