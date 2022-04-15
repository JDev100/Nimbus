using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftPlatform : MonoBehaviour {
    public float start_wait_time;
    private float wait_time;

    public float reset_time;
    private PlatformEffector2D effector;
    
	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.S))
        {
            if (wait_time <= 0)
            {
                effector.rotationalOffset = 180f;
                wait_time = start_wait_time;
            }
            else
            {
                wait_time -= Time.deltaTime;
            }
        }
        if (Input.GetButton("Jump"))
        {
            StartCoroutine(ResetTimer());
        }
	}

    IEnumerator ResetTimer ()
    {
        yield return new WaitForSeconds(reset_time);
        effector.rotationalOffset = 0f;
    }
}
