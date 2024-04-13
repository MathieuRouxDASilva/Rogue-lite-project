using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    //private
    private int _hp;
    private bool _canNotBeHit = false;
    private float _invicibilityTimer = 0;
    private int _nbOfCoin = 0;
    private int _maxHp = 10;

    //public
    public Text text;

    //start
    private void Start()
    {
        text.text = "" + _nbOfCoin.ToString();
        _hp = _maxHp;
    }

    //update
    private void Update()
    {
        HpManager();

        if (_canNotBeHit)
        {
            _invicibilityTimer += Time.deltaTime;
        }

        Timer();
    }

    //see if player is hit
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("LaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            Destroy(other.gameObject);
            HitAndInvicibility();
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("LaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            HitAndInvicibility();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("LaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            IncreaseCoinNumber(1);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("CoinValueOf5"))
        {
            IncreaseCoinNumber(5);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Hp"))
        {
            if (_hp < _maxHp)
            {
                IncreaseHpNumber(1);
                Destroy(other.gameObject);
            }
            
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("ShooterEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("LaserEnnemy"))
        {
            HitAndInvicibility();
        }
        else if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            HitAndInvicibility();
        }
    }


    //what happen if dead
    private void HpManager()
    {
        if (_hp <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("player dead");
        }
    }

    //affect hp and give invicibility for 2 sec
    private void HitAndInvicibility()
    {
        if (!_canNotBeHit)
        {
            _hp--;
            _canNotBeHit = true;
        }
    }

    //timer for the 2 secs
    private void Timer()
    {
        if (_invicibilityTimer >= 2)
        {
            _canNotBeHit = false;
            _invicibilityTimer = 0;
        }
    }

    //function that increase coin amount
    private void IncreaseCoinNumber(int number)
    {
        _nbOfCoin += number;
        text.text = "" + _nbOfCoin.ToString();
    }

    private void IncreaseHpNumber(int number)
    {
        _hp += number;
    }
}