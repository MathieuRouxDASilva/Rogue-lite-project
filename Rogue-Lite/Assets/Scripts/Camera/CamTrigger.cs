using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    //Serializefield
    [SerializeField] private Vector3 newCamPos, newPlayerPos;
    
    //private
    private CamController _camControl;
    
    //public
    public bool isManuallyOpened = false;
    public bool areDoorsOpened = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _camControl = Camera.main.GetComponent<CamController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _camControl.minPos += newCamPos;
            _camControl.maxPos += newCamPos;

            other.transform.position += newPlayerPos;
        }
    }
}
