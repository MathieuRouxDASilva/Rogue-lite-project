using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //private
    private int _hp = 10;
    private int _nbOfCoins = 0;
    private int _nbOfEnnemiesKilled = 0;

    public bool stop = false;

    private void Update()
    {
        HpManager();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            _hp--;
            Debug.Log("hit" + _hp);
        }
        else if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            _hp--;
            Debug.Log("hit" + _hp);
        }
        else if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            _hp--;
            Debug.Log("hit" + _hp);
        }
        else if (other.gameObject.CompareTag("LaserEnnemy"))
        {
            _hp--;
            Debug.Log("hit" + _hp);
        }
        else if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            _hp--;
            Debug.Log("hit" + _hp);
        }
    }


    private void HpManager()
    {
        if (_hp <= 0)
        {
            stop = true;
            Time.timeScale = 0;
        }
    }
}
