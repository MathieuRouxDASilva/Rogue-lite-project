using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    //Serializefiled
    [SerializeField] private GameObject ennemies;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            ennemies.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ennemies.SetActive(false);
    }
}
