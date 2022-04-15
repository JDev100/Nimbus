using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    private Transform target;

    Vector3 velocity = Vector3.zero;

    public float smoothTime = 0.15f;

    //Clamp Variables
    public bool XMaxEnabled = false,
                XMinEnabled = false,
                YMinEnabled = false,
                YMaxEnabled = false;

    public float XMaxValues,
                 XMinValues,
                 YMaxValues,
                 YMinValues,
                 changeRate,
                 cameraSize,
                 defaultRateValue,   
                 defaultSizeValue;

    public CameraBoundsController boundsControl;

    public CameraCollider cameraCollider;

    public Camera theCamera;


    void Awake()
    {
        boundsControl = FindObjectOfType<CameraBoundsController>();
        cameraCollider = FindObjectOfType<CameraCollider>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        changeRate = defaultRateValue;
        cameraSize = defaultSizeValue;
        theCamera.orthographicSize = cameraSize;
    }

    void FixedUpdate()
    {
        Vector3 targetPos = target.position;

        //Horizontal Clamp
        if (YMinEnabled && YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValues, YMaxValues);

        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValues, target.position.y);

        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValues);

        //Vertical Clamp
        if (XMinEnabled && XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValues, XMaxValues);

        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValues, target.position.x);

        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValues);


        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    void Update()
    {
        if (cameraCollider.colliding)
        {
            if (cameraSize < theCamera.orthographicSize)
            {
                theCamera.orthographicSize -= changeRate * Time.deltaTime;
                if (theCamera.orthographicSize < cameraSize)
                    theCamera.orthographicSize = cameraSize;
            }

            else if (cameraSize > theCamera.orthographicSize)
            {
                theCamera.orthographicSize += changeRate * Time.deltaTime;
                if (theCamera.orthographicSize > cameraSize)
                    theCamera.orthographicSize = cameraSize;
            }
        }

        if (!cameraCollider.colliding)
        {
            changeRate = defaultRateValue;

            /*
            if (theCamera.orthographicSize < defaultSizeValue)
            {
                theCamera.orthographicSize += changeRate * Time.deltaTime;
                if (theCamera.orthographicSize > defaultSizeValue)
                    theCamera.orthographicSize = defaultSizeValue;
            }

            if (theCamera.orthographicSize > defaultSizeValue)
            {
                theCamera.orthographicSize -= changeRate * Time.deltaTime;
                if (theCamera.orthographicSize < defaultSizeValue)
                    theCamera.orthographicSize = defaultSizeValue;
            }
            */
        }
    }

}
