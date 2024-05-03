using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //all objects
    [Header("Objects")] [SerializeField] private GameObject player;
    [SerializeField] private GameObject gun;

    //aim and position of mouse
    [Header("Aiming")] [SerializeField] private Vector2 mousePos;
    [SerializeField] private Camera camera;
    
    //private variable
    private Vector2 _playerMoveInput;
    private const float MoveSpeed = 25f;
    private bool _isShooting = false;
    private Rigidbody2D _rb;

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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //update
    private void Update()
    {
        Move();
        Aiming();
        _rb.velocity = Vector2.zero;
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
    
}