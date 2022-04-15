using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public static bool exists;

    public float set_move_speed;
    public float move_speed;
    
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

    AudioSource slide;
    // Use this for initialization
    void Start () {
        slide = FindObjectOfType<SFXManager>().FindSFX("slide");

        //if (!exists)
        //{
        //    exists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        move_speed = set_move_speed;
        my_rigid_body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f && !is_facing_right && can_input)
            FlipCharacter();
        if (Input.GetAxisRaw("Horizontal") < -0.5f && is_facing_right && can_input)
            FlipCharacter();

        if (is_facing_right && !dashing && !sliding)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!is_facing_right && !dashing && !sliding)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update () {
       

        is_moving = false;

            
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f && can_input)
                is_moving = true;
        if ((Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) && can_horizontal && !dashing && can_input
            && !wall_sliding && !GetComponent<WallJump>().hitting_wall)
        {
            my_rigid_body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * move_speed, my_rigid_body.velocity.y);
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
            if ((Input.GetAxisRaw("Horizontal")  == 0f) && can_horizontal && my_rigid_body.velocity.y == 0 
            && GetComponent<PlayerJump>().is_grounded  && can_input && !dashing)
        {
            my_rigid_body.velocity = new Vector2(0, my_rigid_body.velocity.y);
        
        }
           

      
        //For crouch behavior
        if (GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") < -0.5f  && can_input && !(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f))
        {
            crouching = true;
        }
        if (GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") >= 0f && crouching && can_input)
        {
            crouching = false;
        }

        //For slide stuff
        //if (crouching && !sliding
        //    && Input.GetButtonDown("Slide"))
        //{
        //    Slide();
        //}

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Crouch_Idle") && Input.GetButtonDown("Slide"))
        {
            slide.Play();
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
        anim.SetBool("Player_Moving", is_moving);
        anim.SetBool("Crouching", crouching);
    }

    private void FlipCharacter()
    {
        is_facing_right = !is_facing_right;
        //transform.Rotate(Vector3.up, 180);
    }

    private void Slide ()
    {
        slide.Play();
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
}
