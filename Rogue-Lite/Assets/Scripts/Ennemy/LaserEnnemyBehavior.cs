using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserEnnemyBehavior : MonoBehaviour
{
    //Serializefield
    [SerializeField] private GameObject laser;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite calm;
    [SerializeField] private LootManager loot; 
    
    //private 
    private float _timer = 0f;
    private float _shootTimer = 0f;
    private bool _isShooting = false;
    public int _hp = 5;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        RunTheTimer();
        HpManager();
    }


    private void RunTheTimer()
    {
        if (_timer >= 2)
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
            BackToNormalState();
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
        _timer = 0;
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
            Destroy(other.gameObject);
        }
    }

    private void HpManager()
    {
        if (_hp <= 0)
        {
            if (loot != null)
            {
                loot.GenerateLoot(transform.position, transform.rotation);
            }
            Destroy(this. gameObject);
        }
    }
}