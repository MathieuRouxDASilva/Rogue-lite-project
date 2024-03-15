using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAmmoSystem : MonoBehaviour
{
    //ammo stuff
    public int clipSize;
    public int extraAmmo;

    public int currentAmmo;



    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
    }


    private void Update()
    {
        Reload();
    }

    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int numberOfAmmoToReload = clipSize - currentAmmo;
            extraAmmo -= numberOfAmmoToReload;
            currentAmmo += numberOfAmmoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int leftOver = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOver;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}