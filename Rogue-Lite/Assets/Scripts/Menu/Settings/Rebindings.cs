using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rebindings : MonoBehaviour
{
    [SerializeField] private InputActionReference move, shoot;


    private void OnEnable()
    {
        move.action.Disable();
        shoot.action.Disable();
    }

    private void OnDisable()
    {
        move.action.Enable();
        shoot.action.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
