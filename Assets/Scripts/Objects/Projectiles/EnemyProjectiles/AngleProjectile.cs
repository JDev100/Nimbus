using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleProjectile : MonoBehaviour {
    public float horizontal_force;
    public float vertical_force;
    private int direction;

    private Rigidbody2D rb;
    private Transform player;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        

        //Pick direction based on where player is
        if (transform.position.x > player.position.x)
        {
            direction = 0;
        }
        else
        {
            direction = 1;
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (direction == 0)
             rb.AddForce(new Vector2(-horizontal_force, vertical_force));
        else
             rb.AddForce(new Vector2(horizontal_force, vertical_force));
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
