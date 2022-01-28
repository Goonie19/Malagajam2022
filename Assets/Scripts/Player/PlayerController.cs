using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool _keyBoardControls;
    private GameActions _playerControls;
    private PlayerInput _input;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _playerControls = new GameActions();
        _playerControls.Enable();
    }

    public GameActions PlayerControls
    {
        get { return _playerControls; }
    }

    #region Selector de Controles

    public bool KeyBoardControls
    {
        get
        {
            return _keyBoardControls;
        }
    }

    private void Update()
    {
        if (_input.currentControlScheme == _playerControls.KeyboardScheme.name)
            _keyBoardControls = true;
        else
            _keyBoardControls = false;

        //Debug.Log(_playerControls.Player.Movement.ReadValue<Vector2>() + " " + _input.currentControlScheme);


    }

    #endregion

    private void OnEnable()
    {

        _input = GetComponent<PlayerInput>();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        _keyBoardControls = true;
    }

}
