using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{

    public int size;

    public GameObject triggers;
    public CameraBoundsController collisions;
    public GameObject[] boundsControl;


    public float XMinBounds,
                 XMaxBounds,
                 YMinBounds,
                 YMaxBounds,
                 cameraSize,
                 changeRate;

    public bool XMinIsEnabled;
    public bool XMaxIsEnabled;
    public bool YMinIsEnabled;
    public bool YMaxIsEnabled;

    void Start()
    {
      boundsControl = GameObject.FindGameObjectsWithTag("CameraTarget");

      foreach (GameObject triggers in boundsControl)
        {
            collisions = GetComponent<CameraBoundsController>();


        }
    }

}

