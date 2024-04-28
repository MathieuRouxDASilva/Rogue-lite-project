using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserEnnemyBehavior : MonoBehaviour
{
    //Serializefield
    [SerializeField] private GameObject laser;
    [SerializeField] private LootManager loot; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite calm;
    [SerializeField] private Sprite hit;
    
    //private 
    private float _timerLaser = 0f;
    private float _shootTimer = 0f;
    private float _isHitTimer = 0f;
    private int _hp = 5;
    private bool _isShooting = false;
    private bool _isHit = false;

    // Update is called once per frame
    void Update()
    {
        _timerLaser += Time.deltaTime;
        
        RunTheTimer();
        HpManager();
        ChangeSprite();
    }


    private void RunTheTimer()
    {
        BackToNormalState();
        
        if (_timerLaser >= 2)
        {
            Shoot();
            SetAngryState();
        }

        if (_isShooting)
        {
            _shootTimer += Time.deltaTime;
        }

        if (_shootTimer >= 2.5f)
        {
            StopShooting();
        }
        
    }


    private void Shoot()
    {
        laser.gameObject.SetActive(true);
        _isShooting = true;
    }

    private void StopShooting()
    {
        laser.gameObject.SetActive(false);
        _isShooting = false;
        _timerLaser = 0;
        _shootTimer = 0;
    }


    private void SetAngryState()
    {
        spriteRenderer.sprite = angry;
    }

    private void BackToNormalState()
    {
        spriteRenderer.sprite = calm;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            _hp--;
            _isHit = true;
            Destroy(other.gameObject);
        }
    }

    private void ChangeSprite()
    {
        if (_isHit)
        {
            _isHitTimer += Time.deltaTime;
            spriteRenderer.sprite = hit;
        }

        if (_isHitTimer >= 0.2f)
        {
            _isHitTimer = 0;
            _isHit = false;
        }
    }
    
    private void HpManager()
    {
        if (_hp <= 0)
        {
            if (loot != null)
            {
                loot.GenerateLoot(transform.position);
            }
            Destroy(this. gameObject);
        }
    }
}