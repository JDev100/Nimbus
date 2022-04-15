using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static bool exists;

    private GameObject follow_target;
    private Vector3 target_pos;
    public float camera_speed;

    private static bool camera_exists;

    public float start_shake_time;
    private float shake_time;
    public float set_shake_intensity;
    private float shake_intensity;

    //public PolygonCollider2D boundBox;
    //public Vector3 minBounds;
    //public Vector3 maxBounds;

    //private Camera theCam;
    //private float halfHeight;
    //private float halfWidth;
    // Use this for initialization
    void Start () {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
        //minBounds = boundBox.bounds.min;
        //maxBounds = boundBox.bounds.max;

        //theCam = GetComponent<Camera>();
        //halfHeight = theCam.orthographicSize;
        //halfWidth = halfHeight * Screen.width / Screen.height;

        //if (!camera_exists)
        //{
        //    camera_exists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Update()
    {
        

        if (FindObjectOfType<PlayerController>() != null)
            follow_target = FindObjectOfType<PlayerController>().gameObject;
        if (shake_time >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shake_intensity;

            transform.position = new Vector3(transform.position.x, transform.position.y + ShakePos.y, transform.position.z);

            shake_time -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (follow_target != null)
        {
            target_pos = new Vector3(follow_target.transform.position.x, follow_target.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target_pos, camera_speed * Time.deltaTime);
        }

      

        //float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        //float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        //transform.position = new Vector3(clampedX, clampedY, transform.position.z);

    }

    public void ShakeCamera()
    {
        shake_time = start_shake_time;
        shake_intensity = set_shake_intensity;
    }

    //public void SetBounds(PolygonCollider2D newBounds)
    //{
    //    boundBox = newBounds;

    //    minBounds = boundBox.bounds.min;
    //    maxBounds = boundBox.bounds.max;
    //}
}
