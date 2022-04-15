using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour
{
    public Transform spawnPoint;
    private Transform enemyPos;

    void Start()
    {
        enemyPos = GetComponent<Transform>();    
    }


    void OnEnable()
    {
        enemyPos = spawnPoint;    
    }

}
