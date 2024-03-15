using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float fireRate;
    private float _fireRateTimer;
    private float _timer;
    private InputManager _input;
    public bool debug = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletPerShot;
    [SerializeField] private GameObject aimTarget;
    public BulletAmmoSystem ammo;

    // Start is called before the first frame update
    private void Start()
    {
        _input = GetComponent<InputManager>();
        _fireRateTimer = fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
    }

    private bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;

        // if (_fireRateTimer < fireRate)
        // {
        //     debug = true;
        //     return false;
        // }

        if (ammo.currentAmmo == 0)
        {
            return false;
        }

        if (_input.IsShooting)
        {
            return true;
        }

        return false;
    }

    private void Fire()
    {
        _fireRateTimer = 0;
        barrelPosition.LookAt(aimTarget.transform.position);
        ammo.currentAmmo--;
        for (int nbOfShoot = 0; nbOfShoot < bulletPerShot; nbOfShoot++)
        {
            GameObject actualBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
            Rigidbody2D rbe = actualBullet.GetComponent<Rigidbody2D>();
            rbe.AddForce(barrelPosition.forward * bulletVelocity, ForceMode2D.Impulse);
        }
    }
}
