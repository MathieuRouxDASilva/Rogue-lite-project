using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKilledManager : MonoBehaviour
{
    private List<GameObject> _listOfLasers;
    private List<GameObject> _listOfShooters;
    private List<GameObject> _listOfPatterns;
    private List<GameObject> _listOfChasers;
    private bool _loadOnce = false;
    private float _timer = 0;

    private void Count()
    {
        var count = _listOfChasers.Count + _listOfLasers.Count + _listOfPatterns.Count + _listOfShooters.Count;

        ChangeLengthList();
        
        if (count == 0)
        {
            //load winning scene
            Debug.Log("win");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    private void ChangeLengthList()
    {
        foreach (var enemy in _listOfChasers)
        {
            if (enemy == null)
            {
                _listOfChasers.Remove(enemy.gameObject);
            }
        }
        foreach (var enemy in _listOfShooters)
        {
            if (enemy == null)
            {
                _listOfShooters.Remove(enemy.gameObject);
            }
        }
        foreach (var enemy in _listOfLasers)
        {
            if (enemy == null)
            {
                _listOfLasers.Remove(enemy.gameObject);
            }
        }
        foreach (var enemy in _listOfPatterns)
        {
            if (enemy == null)
            {
                _listOfPatterns.Remove(enemy.gameObject);
            }
        }
    }
    
    private void LateUpdate()
    {
        if (_timer >= 2f)
        {
            if (!_loadOnce)
            {
                //this is done in late update because otherwise it bug like crazy
                _listOfChasers = GameObject.FindGameObjectsWithTag("ChaserEnnemy").ToList();
                _listOfShooters = GameObject.FindGameObjectsWithTag("ShooterEnnemy").ToList();
                _listOfPatterns = GameObject.FindGameObjectsWithTag("PatternEnnemy").ToList();
                _listOfLasers = GameObject.FindGameObjectsWithTag("LaserEnnemy").ToList();
                _loadOnce = true;

            }
            Count();   
        }

        if (_timer <= 3f)
        {
            _timer += Time.deltaTime;
        }
        
    }
}
