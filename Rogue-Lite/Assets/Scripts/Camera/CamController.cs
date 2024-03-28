using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    //serializefield
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed;
    
    //public
    public Vector3 minPos, maxPos;

    //private
    private Vector3 _targetPos, _newPos;


    private void LateUpdate()
    {
        if (transform.position != player.transform.position)
        {
            _targetPos = player.transform.position;

            Vector3 camBoundaryPos = new Vector3(
                Math.Clamp(_targetPos.x, minPos.x, maxPos.x),
                Math.Clamp(_targetPos.y, minPos.y, maxPos.y),
                Math.Clamp(_targetPos.z, minPos.z, maxPos.z));

            _newPos = Vector3.Lerp(transform.position, camBoundaryPos, smoothSpeed);
            transform.position = _newPos;
        }
    }
}