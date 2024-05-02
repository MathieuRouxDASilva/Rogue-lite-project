using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private List<GameObject> _allDoors;
    private int i;

    private void LateUpdate()
    {
        _allDoors = GameObject.FindGameObjectsWithTag("doors").ToList();
    }


    private void CalculateChildrens()
    {
        var list = gameObject.GetComponentsInChildren<Transform>();
        i = list.Length -1;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CalculateChildrens();
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
        }
        else if (i == 0)
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
    
}