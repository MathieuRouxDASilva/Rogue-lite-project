using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //serializeField/public variable
    [SerializeField] private Rigidbody2D rb;
    
    //private variable
    private float _verticalInput = 0f;
    private float _horizontalInput = 0f;
    private float _moveSpeed = 50f;

    //playerInput
    private void OnMoveVertical(InputValue value)
    {
        _verticalInput = value.Get<float>();
    }
    private void OnMoveHorizontal(InputValue value)
    {
        _horizontalInput = value.Get<float>();
        Debug.Log(_horizontalInput);
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        rb.velocity += new Vector2(_horizontalInput, _verticalInput) * (Time.deltaTime * _moveSpeed);

        if (_horizontalInput < 0f)
        {
            rb.velocityX *= -1f;
        }
        
        Cap();
    }

    private void Cap()
    {
        if (_horizontalInput <= 0.2)
        {
            _horizontalInput = 0f;
        }
        else if (_verticalInput <= 0.2f)
        {
            _verticalInput = 0f;
        }
    }
}
