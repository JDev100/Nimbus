using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public bool colliding;

    public SmartCamera smartCam;

    public CameraBoundsController boundsControl;

    public Camera theCamera;

    void Start()
    {
        smartCam = FindObjectOfType<SmartCamera>();   
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraTarget")
        {
            boundsControl = collision.gameObject.GetComponent<CameraBoundsController>();

            smartCam.XMaxEnabled = boundsControl.XMaxIsEnabled;
            smartCam.XMinEnabled = boundsControl.XMinIsEnabled;
            smartCam.YMinEnabled = boundsControl.YMinIsEnabled;
            smartCam.YMaxEnabled = boundsControl.YMaxIsEnabled;

            smartCam.XMaxValues = boundsControl.XMaxBounds;
            smartCam.XMinValues = boundsControl.XMinBounds;
            smartCam.YMaxValues = boundsControl.YMaxBounds;
            smartCam.YMinValues = boundsControl.YMinBounds;

            smartCam.cameraSize = boundsControl.cameraSize;
            smartCam.changeRate = boundsControl.changeRate;

            colliding = true;
      
        }

    }


    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "CameraTarget")
        {
            smartCam.XMaxEnabled = false;
            smartCam.XMinEnabled = false;
            smartCam.YMinEnabled = false;
            smartCam.YMaxEnabled = false;

            smartCam.XMaxValues = 0;
            smartCam.XMinValues = 0;
            smartCam.YMaxValues = 0;
            smartCam.YMinValues = 0;


            Debug.Log("Exit Collide");
        }
    }
}
