using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {
    public float knockback_x;
    public float knockback_y;
    public float start_knockback_time;
    private float knockback_time;

    public bool has_knockback;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (knockback_time <= 0)
        {

            GetComponent<PlayerController>().can_input = true;
            GetComponent<NimbusSlash>().can_input = true;
            GetComponent<NimbusFowardMagic>().can_input = true;
            GetComponent<Dash>().can_input = true;
            GetComponent<PlayerJump>().can_input = true;
            GetComponent<WallJump>().can_input = true;
            GetComponent<ArcFire>().can_input = true;
        }
        else
        {
            knockback_time -= Time.deltaTime;
            //Disable controls during knockback
            GetComponent<PlayerController>().can_input = false;
            GetComponent<NimbusSlash>().can_input = false;
            GetComponent<NimbusFowardMagic>().can_input = false;
            GetComponent<Dash>().can_input = false;
            GetComponent<PlayerJump>().can_input = false;
            GetComponent<WallJump>().can_input = false;
            GetComponent<ArcFire>().can_input = false;
        }

        if (has_knockback && knockback_time <= 0)
        {
            if (GetComponent<PlayerJump>().is_grounded)
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else if (!GetComponent<PlayerJump>().is_grounded)
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);

            has_knockback = false;
        }
	}

    public void DoKnockBack(int direction)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        knockback_time = start_knockback_time;

        has_knockback = true;

        if (direction == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback_x, knockback_y);
        }
        else if (direction == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockback_x, knockback_y);
        }
    }
}
