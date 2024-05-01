using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private List<GameObject> _allDoors;
    private int i;

    private void LateUpdate()
    {
        _allDoors = GameObject.FindGameObjectsWithTag("doors").ToList();
        var list = gameObject.GetComponentsInChildren<Transform>();
        
        i = list.Length;
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
            Debug.Log("hello player");
            foreach (var door in _allDoors)
            {
                door.SetActive(false);
            }
        }
        else if (i == 0)
        {
            Debug.Log("re-opening");
            foreach (var door in _allDoors)
            {
                door.SetActive(true);
            }
        }
    }
    
}