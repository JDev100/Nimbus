using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("spawnpoint").GetComponent<Transform>();

        spawnPoint.position = transform.position;  
    }
}
