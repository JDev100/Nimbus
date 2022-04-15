using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPropell : MonoBehaviour {
    public float jump_velocity;

    private Rigidbody2D rb;

    public bool evil_nimbus = false;
    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!evil_nimbus)
        {
            if (other.gameObject.tag == "EnemyHealth")
            {
                Propel();
            }
        }
        else
        {
            if (other.gameObject.tag == "PlayerHealth")
            {
                Propel();
            }
        }
       
    }

    private void Propel()
    {
        Debug.Log("Propel");

        rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
    }
}
