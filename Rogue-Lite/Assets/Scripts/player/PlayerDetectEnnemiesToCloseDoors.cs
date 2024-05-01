using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetectEnnemiesToCloseDoors : MonoBehaviour
{
    //Serializefield
    [SerializeField] private RoomManager rooms;

    //private
    private List<GameObject> _listofdoors;
    
    
    private void Start()
    {
        _listofdoors = GameObject.FindGameObjectsWithTag("doors").ToList();
        Debug.Log("stp");
    }

    private void Update()
    {
        if (_listofdoors is not null)
        {
            foreach (var door in _listofdoors)
            {
                Debug.Log("door : " + door.transform.position);
            }   
        }
        else
        {
            Debug.Log("sa");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ChaserEnnemy") || other.CompareTag("ShooterEnnemy") ||
            other.CompareTag("PatternEnnemy") || other.CompareTag("LaserEnnemy"))
        {
            
        }
    }
}