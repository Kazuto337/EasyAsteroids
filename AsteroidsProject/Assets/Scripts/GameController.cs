using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Ateroid Spawner")]
    [SerializeField] GameObject hazard;
    [SerializeField] Vector3 spawnValues;

    public void HazardSpawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnValues.x , spawnValues.x), Random.Range(5.5f , 5) , 0);
        Instantiate(hazard , spawnPos , Quaternion.identity);
    }
}
