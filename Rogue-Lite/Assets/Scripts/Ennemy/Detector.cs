using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool isSeeing = false;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            isSeeing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isSeeing = false;
    }
}