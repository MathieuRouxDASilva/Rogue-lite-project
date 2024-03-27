using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    //all objects
    [Header("Objects")] [SerializeField] private GameObject player;
    [SerializeField] private GameObject gun;

    //aim and position of mouse
    [Header("Aiming")] [SerializeField] private Vector2 mousePos;
    [SerializeField] private new Camera camera;

    //animator related stuff
    [Header("Animator related")] [SerializeField]
    private Animator animator;

    //private variable
    private Vector2 _playerMoveInput;
    private const float MoveSpeed = 25f;
    private bool _isShooting = false;

    //get is shooting
    public bool IsShooting => _isShooting;


    //playerInput
    private void OnMove(InputValue value)
    {
        _playerMoveInput = value.Get<Vector2>();
    }

    private void OnShoot(InputValue value)
    {
        if (value.Get<float>() > 0.2)
        {
            _isShooting = true;
        }
        else
        {
            _isShooting = false;
        }
    }

    //update
    private void Update()
    {
        Move();
        Aiming();
        Shoot();
    }

    //move function
    private void Move()
    {
        player.transform.position += (Vector3)_playerMoveInput * (float)(MoveSpeed * 0.5 * Time.deltaTime);

        if (_playerMoveInput.x < 0.5 || _playerMoveInput.y < 0.5)
        {
            player.transform.position += new Vector3(0, 0, 0);
        }
        
    }

    //function that aim gun at mouse pos
    private void Aiming()
    {
        mousePos = Input.mousePosition;

        Vector3 objectPos = camera.WorldToScreenPoint(gun.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        if (_isShooting)
        {
            animator.SetBool("Shooting", true);
            
            
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }
}