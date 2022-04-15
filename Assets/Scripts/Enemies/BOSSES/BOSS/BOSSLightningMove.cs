using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSLightningMove : MonoBehaviour {
    public bool right;
    public float speed;

    private Rigidbody2D rb;

  

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
       

        if (right)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
