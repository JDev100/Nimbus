using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusController : MonoBehaviour {
    public float set_move_speed;
    public float move_speed;
    public bool press_move = false;
    public bool press_crouch = false;

    private Rigidbody2D my_rigid_body;
    private Animator anim;

    //For allowing and disallowing inputs
    public bool can_input = true;

    //For ignoring horzontal input when dashing;
    public bool dashing = false;

    //For crouch behavior
    public bool crouching;
    public float slide_speed;
    public bool sliding;
    public float start_slide_time;
    private bool has_slided;
    private float slide_time;
    private int direction;

    //For ignoring horizontal input when wall sliding
    public bool wall_sliding = false;
    public bool can_horizontal;

    public bool is_facing_right = true;
    private bool is_moving = false;

    void Start()
    {
        move_speed = set_move_speed;
        my_rigid_body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //if (Input.GetAxisRaw("Horizontal") > 0.5f && !is_facing_right && can_input)
        //    FlipCharacter();
        //if (Input.GetAxisRaw("Horizontal") < -0.5f && is_facing_right && can_input)
        //    FlipCharacter();

        if (can_input)
        FacePlayer();

        if (is_facing_right && !dashing /*&& !sliding*/)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!is_facing_right && !dashing /*&& !sliding*/)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {


        is_moving = false;


        if (press_move && can_input)
        {
            is_moving = true;
        }
            
        if (press_move /*&& can_horizontal*/ && !dashing  && can_input && !crouching
           /* && !wall_sliding*/)
        {

            my_rigid_body.velocity = new Vector2(direction * move_speed, my_rigid_body.velocity.y);
        }

        if (!press_move && !dashing && !sliding)
        {
            my_rigid_body.velocity = new Vector2(0, my_rigid_body.velocity.y);
        }
        //    if (Input.GetAxisRaw("Horizontal") > 0.5f && (can_horizontal && !dashing) && can_input)
        //{
        //    if (!wall_sliding && is_facing_right)
        //        my_rigid_body.velocity = new Vector2(move_speed, my_rigid_body.velocity.y);
        //}
        //if (Input.GetAxisRaw("Horizontal") < -0.5f && (can_horizontal && !dashing) && can_input)
        //{
        //    if (!wall_sliding && !is_facing_right)
        //        my_rigid_body.velocity = new Vector2(-move_speed, my_rigid_body.velocity.y);
        //}
        //if ((Input.GetAxisRaw("Horizontal") == 0f) && can_horizontal && my_rigid_body.velocity.y == 0
        //&& GetComponent<PlayerJump>().is_grounded && can_input)
        //    my_rigid_body.velocity = new Vector2(0, my_rigid_body.velocity.y);


        //For crouch behavior
        if (GetComponent<EvilNimbusJump>().is_grounded && press_crouch && can_input && !press_move)
        {
            crouching = true;
        }
        if (!press_crouch)
        {
            crouching = false;
        }

        //For slide stuff
        if (crouching && !sliding
            && Input.GetButtonDown("Jump"))
        {
            Slide();
        }
        if (slide_time <= 0f)
        {
            sliding = false;
            if (has_slided)
            {
                my_rigid_body.velocity = new Vector2(0, my_rigid_body.velocity.y);
                has_slided = false;
            }
        }
        else
        {
            slide_time -= Time.deltaTime;
            my_rigid_body.velocity = new Vector2(slide_speed * direction, my_rigid_body.velocity.y);


        }

        anim.SetBool("Sliding", sliding);
        //anim.SetBool("Player_Moving", is_moving);
        //anim.SetBool("Crouching", crouching);
    }

    private void FlipCharacter()
    {
        is_facing_right = !is_facing_right;
        //transform.Rotate(Vector3.up, 180);
    }

    private void Slide()
    {
        sliding = true;
        slide_time = start_slide_time;
        has_slided = true;

        if (is_facing_right)
        {
            direction = 1;
            transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = -1;
        }
    }

    public void FacePlayer()
    {
        if (FindObjectOfType<PlayerController>().transform.position.x > transform.position.x)
        {
            is_facing_right = true;
            direction = 1;
        }
        else
        {
            is_facing_right = false;
            direction = -1;
        }
    }
}
