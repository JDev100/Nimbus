using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusAnimationHandler : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rb;

    private bool is_wall_jumping;
    private bool is_dashing;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Animations for jumping
        if (rb.velocity.y < 0)
        {
            anim.SetBool("Player_Falling", true);
            anim.SetBool("Player_Jumping", false);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Player_Falling", false);
            anim.SetBool("Player_Jumping", true);
        }
        else if (rb.velocity.y == 0)
        {
            anim.SetBool("Player_Falling", false);
            anim.SetBool("Player_Jumping", false);
        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool("Player_Moving", true);
        }
        else
        {
            anim.SetBool("Player_Moving", false);
        }

        //For Crouching
        if (GetComponent<EvilNimbusController>().crouching)
        {
            anim.SetBool("Crouching", true);
        }
        else
        {
            anim.SetBool("Crouching", false);
        }

        //SetAnimation for wall slide
        //anim.SetBool("Wall_Sliding", GetComponent<WallJump>().wall_sliding);

        //anim.SetBool("Wall_Jumping", is_wall_jumping);

        //Animation for dash
        if (GetComponent<EvilNimbusController>().dashing)
        {
            anim.SetBool("Dashing", true);
        }
        else
        {
            anim.SetBool("Dashing", false);
        }
       

        //Checking for on ground
        anim.SetBool("Is_On_Ground", GetComponent<EvilNimbusJump>().is_grounded);

        ////Set proper rotation for wall jump
        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_WallJump")
        //    && GetComponent<WallJump>().has_wall_jumped)
        //{
        //    if (GetComponent<Rigidbody2D>().velocity.x > 0)
        //    {
        //        transform.rotation = transform.rotation = Quaternion.Euler(0, -180, 0);
        //    }
        //    else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        //    {
        //        transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);

        //    }

        //}
        if (GetComponent<EvilNimbusController>().dashing)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                transform.rotation = transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Ground"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Magic_Forward"))
        {
            rb.velocity = new Vector2(0, 0);
        }
        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Air"))
        //{

        //    rb.velocity = Vector2.zero;
        //}

        //For crouching
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Crouch") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Crouch_Idle")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Crouch_Slash"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //For Knockback stuff
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_TakeDamage_Air"))
        {
            GetComponent<EvilNimbusInputManager>().SwitchInput(false);
        }
        else if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_TakeDamage_Air") /*&& /*!GetComponent<Knockback>().has_knockback*/)
        {
            GetComponent<EvilNimbusInputManager>().SwitchInput(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (GetComponent<WallJump>().kick_time > 0)
        //{
        //    is_wall_jumping = true;
        //}
        //else
        //{
        //    is_wall_jumping = false;
        //}
    }
}
