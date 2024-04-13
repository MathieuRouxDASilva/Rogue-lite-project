using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawnTrainingRoom : MonoBehaviour
{
    //Serializefield
    [Header("ennemies")]
    [SerializeField] private GameObject chaser;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject patternGuy;
    [SerializeField] private GameObject laserGuy;
    [SerializeField] private Transform spawnPoint;


    public void SpawnChaser()
    {
        Instantiate(chaser, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnPattern()
    {
        Instantiate(patternGuy, spawnPoint.position, spawnPoint.rotation);
    }
    
    public void SpawnShooter()
    {
        Instantiate(shooter, spawnPoint.position, spawnPoint.rotation);
    }
    
    public void SpawnLaser()
    {
        Instantiate(laserGuy, spawnPoint.position, spawnPoint.rotation);
    }

}
