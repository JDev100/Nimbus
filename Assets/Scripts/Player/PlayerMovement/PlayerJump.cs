using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    public float jump_velocity;
    public float fall_multiplier = 2.5f;
    public float low_jump_multiplier = 2f;

    public Transform ground_check;
    public float ground_check_radius;
    private bool is_jumping;
    private bool has_jumped;
    public LayerMask what_is_ground;
    public bool is_grounded;

    private AudioSource land_sound;
    //For controlling input
    public bool can_input = true;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        land_sound = FindObjectOfType<SFXManager>().FindSFX("land");

        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.transform.position, ground_check_radius, what_is_ground);
    }

    // Update is called once per frame
    void Update () {
		if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (low_jump_multiplier - 1) * Time.deltaTime;
        } 
        if (Input.GetButtonDown("Jump") && is_grounded && can_input && !GetComponent<PlayerController>().dashing && !GetComponent<PlayerController>().crouching
            && !GetComponent<PlayerController>().sliding)
        {
            Jump();
        }

        //Check if player has been in air and stop horizontal velocity when on ground
        if (rb.velocity.y != 0f)
        {
            has_jumped = true;
        }
        ////Horizontal movement stops when not inputing in air
        //if (has_jumped && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) && can_input && !GetComponent<WallJump>().has_wall_jumped)
        //{
        //    rb.velocity = new Vector2(0, rb.velocity.y);
        //}
        //else
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        //}

        if (has_jumped && rb.velocity.y == 0f)
        {
            if (is_grounded)
            land_sound.Play();
            rb.velocity = new Vector2(0, rb.velocity.y);
            has_jumped = false;
        }
        

    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
    }
}
