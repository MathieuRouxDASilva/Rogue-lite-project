using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternEnnemyBehavior : MonoBehaviour
{
    //serializefield
    [SerializeField] private float speed = 0.2f;
    //[SerializeField] private PlayerData player;
    
    //private
    private int _listIndex = 0;
    private int _nbMaxOfPoint;
    private float _minDistance = 0.5f;
    private List<GameObject> _listOfDestinations;
   
    
    
    private void Start()
    {
        _listOfDestinations = GameObject.FindGameObjectsWithTag("spawn").ToList();
    }


    // Update is called once per frame
    void Update()
    {
        CapSystem();
        RunToNextPoint();
    }

    //system that cap the list at 4 elements otherwise it wil move in the entire map
    private void CapSystem()
    {
        if (_listOfDestinations.Count > 4)
        {
            foreach (var point in _listOfDestinations)
            {
                _nbMaxOfPoint = _listOfDestinations.Count;
                
                if (_nbMaxOfPoint > 4)
                {
                     _listOfDestinations.Remove(point);
                }
            }
        }
    }
    
    
    private void RunToNextPoint()
    {
        //setup
        Vector3 destination = _listOfDestinations[_listIndex].transform.position;
        Vector3 path = destination - transform.position;
        float relativePosition = Vector3.Distance(transform.position, destination);

        //if needed -> go to next point
        if (relativePosition <= _minDistance)
        {
            ChangeDestination();
        }
        else
        {
            //move
            transform.Translate(path * (speed * Time.deltaTime), Space.World);
        }
    }

    private void ChangeDestination()
    {
        if (_listIndex >= _listOfDestinations.Count -1)
        {
            _listIndex = 0;
        }
        else
        {
            _listIndex++;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
