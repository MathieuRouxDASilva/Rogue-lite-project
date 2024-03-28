using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBulletInteraction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
