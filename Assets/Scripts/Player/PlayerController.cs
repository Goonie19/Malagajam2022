using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3f;

    public Animator Anim;

    public bool PositivePoleUp
    {
        get => _positivePoleUp;
    }

    private bool _positivePoleUp;

    private float _moveDirection;

    private void Awake()
    {
        _positivePoleUp = true;
        GameManager.Instance.PlayerReference = gameObject;
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
        transform.Translate( Speed * _moveDirection * Vector2.right * Time.deltaTime);
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
}
