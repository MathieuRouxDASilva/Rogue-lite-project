using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletModel;
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private Transform gunHole;

    private Vector2 _bulletSpeed = new Vector2();
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity += _bulletSpeed;
    }
    
}
