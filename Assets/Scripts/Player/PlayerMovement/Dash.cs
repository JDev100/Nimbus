using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {
    public float speed;
    public float start_dash_time;
    private float dash_time;
    private int direction;

    //For dash cooldown
    private bool has_dashed;


    //Can only use when unlocked
    public bool unlocked = false;

    //For controlling input
    public bool can_input = true;

    //For resetting gravity
    private float reset_gravity;

    private Rigidbody2D rb;

    //SFX
    private AudioSource dash_sound;
	// Use this for initialization
	void Start () {
        dash_sound = FindObjectOfType<SFXManager>().FindSFX("dash");

        //Get gravity for resetting
        reset_gravity = GetComponent<Rigidbody2D>().gravityScale;

        rb = GetComponent<Rigidbody2D>();
        dash_time = start_dash_time;
	}
	
	// Update is called once per frame
	void Update () {
        //For dash cool down
        if (GetComponent<PlayerJump>().is_grounded)
        {
            can_input = true;
            has_dashed = false;
        }
        if (GetComponent<WallJump>().has_wall_jumped)
        {
            has_dashed = false;
        }
        if (rb.velocity.y != 0 && has_dashed)
        {
            can_input = false;
        }

        if (unlocked)
        {

            if (direction == 0)
            {
                if (Input.GetButtonDown("Fire3") && can_input)
                {
                    if (Input.GetAxisRaw("Horizontal") < -0.5f && can_input)
                    {
                        dash_sound.Play();
                        direction = 1;
                        has_dashed = true;
                        rb.velocity = Vector2.zero;
                    }
                    else if (Input.GetAxisRaw("Horizontal") > 0.5f && can_input)
                    {
                        dash_sound.Play();
                        direction = 2;
                        has_dashed = true;
                        rb.velocity = Vector2.zero;
                    }
                    else if (Input.GetAxisRaw("Horizontal") == 0 && can_input)
                    {
                        if (GetComponent<PlayerController>().is_facing_right)
                        {
                            dash_sound.Play();
                            direction = 2;
                            has_dashed = true;
                            rb.velocity = Vector2.zero;
                        }
                        else
                        {
                            dash_sound.Play();
                            direction = 1;
                            has_dashed = true;
                            rb.velocity = Vector2.zero;
                        }
                    }
                }
            }
            else 
            {
                if (!GetComponent<PlayerJump>().is_grounded && GetComponent<WallJump>().hitting_wall)
                {
                    DashCancel(false);
                }
                if (Input.GetButton("Jump") && GetComponent<PlayerJump>().is_grounded)
                {
                    DashCancel(true);
                }
                if (dash_time <= 0)
                {
                    rb.gravityScale = reset_gravity;

                    direction = 0;
                    dash_time = start_dash_time;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    GetComponent<PlayerController>().dashing = false;
                }
                else
                {
                    

                    //Gravity is 0 during dash
                    rb.gravityScale = 0f;

                    dash_time -= Time.deltaTime;

                    GetComponent<PlayerController>().dashing = true;

                    if (direction == 1)
                    {
                        rb.velocity = new Vector2(-speed, 0);
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = new Vector2(speed, 0);
                    }
                }

            }
        }
	}

    private void DashCancel(bool jump)
    {
        Debug.Log("Dash Cancelling");
        rb.gravityScale = reset_gravity;

        direction = 0;
        dash_time = 0;
        GetComponent<PlayerController>().dashing = false;
        if (jump)
        GetComponent<PlayerJump>().Jump();
    }
}
