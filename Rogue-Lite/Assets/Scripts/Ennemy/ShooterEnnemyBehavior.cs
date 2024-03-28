using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShooterEnnemyBehavior : MonoBehaviour
{
    //SerializeField
    [SerializeField] private GameObject player;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private float ennemyBulletVelocity;
    [SerializeField] private int bulletPerShot;

    //private
    private float _fireRateTimer;
    private float _timer;
    private int _hp = 1;

    private void Start()
    {
        _fireRateTimer = fireRate;
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Update()
    {
        if (ShouldFire())
        {
            ShootAtPlayer();
        }

        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;

        if (_fireRateTimer < fireRate)
        {
            return false;
        }

        return true;
    }

    private void ShootAtPlayer()
    {
        _fireRateTimer = 0;
        barrelPosition.LookAt(player.transform.position);
        for (int nbOfShoot = 0; nbOfShoot < bulletPerShot; nbOfShoot++)
        {
            GameObject actualBullet = Instantiate(bullet, barrelPosition.position,
                new Quaternion(barrelPosition.rotation.x, barrelPosition.rotation.y + 90f, barrelPosition.rotation.z,
                    barrelPosition.rotation.w));
            Rigidbody2D rbe = actualBullet.GetComponent<Rigidbody2D>();
            rbe.AddForce(barrelPosition.forward * (ennemyBulletVelocity * 5), ForceMode2D.Impulse);
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            _hp--;
            Destroy(other.gameObject);
        }
    }
}