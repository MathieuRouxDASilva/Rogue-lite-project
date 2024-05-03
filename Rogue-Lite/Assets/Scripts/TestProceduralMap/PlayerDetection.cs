using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    //private
    private int i;
    private float _timer;
    private bool _isPlayerSeen;
    private bool _analysedOnce;
    private Transform[] list;
    private List<GameObject> _allDoors;

    //Serializefiled
    [SerializeField] private TextMeshPro text;


    private void LateUpdate()
    {
        _allDoors = GameObject.FindGameObjectsWithTag("doors").ToList();
    }

    private void Update()
    {
        //_timer++;
        StartTimer();
    }

    private void CalculateChildrens()
    {
        list = gameObject.GetComponentsInChildren<Transform>();
        i = list.Length - 1;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isPlayerSeen)
        {
            _timer = 0;
        }

        if (!_analysedOnce)
        {
            CalculateChildrens();
        }
        
        if (other.gameObject.CompareTag("player") && i > 0)
        {
            foreach (var door in _allDoors)
            {
                door.GetComponent<CamTrigger>().isManuallyOpened = false;
                if (door.activeSelf == true)
                {
                    door.GetComponent<CamTrigger>().isManuallyOpened = true;
                    door.GetComponent<Renderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }

            _isPlayerSeen = true;
        }
        if (i == 0 || _timer >= 20)
        {
            foreach (var door in _allDoors)
            {
                if (door.GetComponent<CamTrigger>().isManuallyOpened)
                {
                    door.GetComponent<CamTrigger>().isManuallyOpened = false;
                    door.GetComponent<Renderer>().enabled = true;
                    door.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            _isPlayerSeen = false;
        }
    }

    private void StartTimer()
    {
        if (_isPlayerSeen)
        {
            _timer += 1 * Time.deltaTime;
        }

        if (_timer >= 21)
        {
            _timer = 21;
        }
    }
}