using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {
    public float jump_force;
    public float kick_force;
    public float wall_slide_speed;
    public float start_kick_time;
    //Animation handler accesses this
    public float kick_time;

    public Transform wall_check;
    public float wall_check_radius;
    public LayerMask what_is_wall;
    public bool hitting_wall;

    public bool wall_sliding;
    public bool has_wall_jumped;

    //For controlling input
    public bool can_input = true;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        hitting_wall = Physics2D.OverlapCircle(wall_check.transform.position, wall_check_radius, what_is_wall);

        if (hitting_wall && !GetComponent<PlayerJump>().is_grounded && !wall_sliding)
        {
            GetComponent<PlayerController>().can_horizontal = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            //If you press jump while sliding
            if (Input.GetButtonDown("Jump") && can_input)
            {
                WallKick();
            }
        }
        
        //What to do when sliding on wall
        else if (wall_sliding)
        {
            GetComponent<PlayerController>().can_horizontal = false;
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && can_input)
            {
                WallKick();
            }

            if (rb.velocity.y < -wall_slide_speed)
            {
                rb.velocity = new Vector2(0, -wall_slide_speed);
            }

          
        }

        //Disable horizontal for brief moment when kicking
        if (kick_time > 0)
        {
            kick_time -= Time.deltaTime;
            GetComponent<PlayerController>().can_horizontal = false;
        }
        else
            GetComponent<PlayerController>().can_horizontal = true;

    }

    // Update is called once per frame
    void Update () {
        GetComponent<PlayerController>().wall_sliding = wall_sliding;
        //Checks if you are touching wall, in air, and falling
		if (hitting_wall && !GetComponent<PlayerJump>().is_grounded && rb.velocity.y < 0)
        {
            //Wall slide if you press against wall
            if ((GetComponent<PlayerController>().is_facing_right && Input.GetAxisRaw("Horizontal") > 0.5f)
                || (!GetComponent<PlayerController>().is_facing_right && Input.GetAxisRaw("Horizontal") < -0.5f) && can_input)
            {
                wall_sliding = true;
            }
            else
            {
                wall_sliding = false;
            }
        }
        else
        {
            wall_sliding = false;
        }

       
        
        //When you have wall_jumped
        if (has_wall_jumped)
        {
            GetComponent<PlayerController>().move_speed = kick_force;
        }
        if (has_wall_jumped && GetComponent<PlayerJump>().is_grounded)
        {
            GetComponent<PlayerController>().move_speed = GetComponent<PlayerController>().set_move_speed;
            has_wall_jumped = false;
        }
	}

    void WallKick ()
    {
        kick_time = start_kick_time;
        //Different direction based on which way you are facing
        if (GetComponent<PlayerController>().is_facing_right)
        {
            Debug.Log("Shoot_Left");
            has_wall_jumped = true;
            GetComponent<PlayerController>().can_horizontal = false;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(-kick_force, jump_force);
        }
        else
        {
            Debug.Log("Shoot_Right");
            has_wall_jumped = true;
            GetComponent<PlayerController>().can_horizontal = false;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(kick_force, jump_force);
        }
    }
}
