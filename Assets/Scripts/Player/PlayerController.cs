using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3f;

    private void Update()
    {
        transform.Translate(InputManager.Instance.PlayerControls.Player.Movement.ReadValue<float>() * Vector2.right * Time.deltaTime);
    }



}
