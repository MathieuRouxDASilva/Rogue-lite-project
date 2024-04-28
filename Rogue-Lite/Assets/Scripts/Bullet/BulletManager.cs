using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    //SerializeField
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletPerShot;
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private PlayerData playerData;

    //private
    private float _fireRateTimer;
    private float _timer;
    private InputManager _input;

    //public
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

        if (_fireRateTimer < fireRate)
        {
            return false;
        }

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
            if (playerData.gotUpgraded)
            {
                GameObject actualUpgradeBullet = Instantiate(bullet, barrelPosition.position,
                    new Quaternion(barrelPosition.rotation.x, barrelPosition.rotation.y -180, barrelPosition.rotation.z,
                        barrelPosition.rotation.w));
                actualUpgradeBullet.transform.localScale *= 2;
                Rigidbody2D rb = actualUpgradeBullet.GetComponent<Rigidbody2D>();
                rb.AddForce(barrelPosition.forward * (bulletVelocity * 5), ForceMode2D.Impulse);
            }
            else
            {
                GameObject actualBullet = Instantiate(bullet, barrelPosition.position,
                                new Quaternion(barrelPosition.rotation.x, barrelPosition.rotation.y -180, barrelPosition.rotation.z,
                                    barrelPosition.rotation.w));
                Rigidbody2D rbe = actualBullet.GetComponent<Rigidbody2D>();
                rbe.AddForce(barrelPosition.forward * (bulletVelocity * 5), ForceMode2D.Impulse);
            }
        }
    }
}