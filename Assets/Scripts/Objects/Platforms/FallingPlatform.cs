using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
    public float time_to_fall;
    public float fall_gravity;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("PlatformFall", time_to_fall);
        }
    }

    private void PlatformFall()
    {
        rb.gravityScale = fall_gravity;
    }
}
