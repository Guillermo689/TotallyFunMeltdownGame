using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    internal PlayerInput _playerInput;
    internal InputAction _jump;
    internal InputAction _move;
    internal InputAction _duck;
    internal InputAction _pause;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _jump = _playerInput.actions["Jump"];
        _move = _playerInput.actions["Move"];
        _duck = _playerInput.actions["Duck"];
        _pause = _playerInput.actions["Pause"];
    }

    
}
