using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform startPos;
    private Vector3 nextTarget;
    public float speed;

    void Start()
    {
        nextTarget = startPos.position;
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextTarget = pos2.position;
            Debug.Log("nextTarget = pos2.position");
        }

        if(transform.position == pos2.position)
        {
            nextTarget = pos1.position;
            Debug.Log("nextTarget = pos1.position");
        }

        transform.position = Vector3.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
  
}
