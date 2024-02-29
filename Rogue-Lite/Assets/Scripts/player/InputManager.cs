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

    [SerializeField] private BulletBehavior bullet;

    //aim and position of mouse
    [Header("Aiming")] [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector2 mouseWorldPos;
    [SerializeField] private new Camera camera;

    [SerializeField] private Transform gunHole;

    //animator related stuff
    [Header("Animator related")] [SerializeField]
    private Animator animator;

    //private variable
    private Vector2 _playerMoveInput;
    private const float MoveSpeed = 25f;
    private bool _isShooting = false;

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
        player.transform.position += (Vector3)_playerMoveInput * (MoveSpeed * Time.deltaTime);
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

            var placeHolder = Instantiate(bullet, gunHole);
            
            placeHolder.transform.rotation = gunHole.transform.rotation;
            
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }
}