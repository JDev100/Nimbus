using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSDash : MonoBehaviour {
    public float speed;
    public float start_dash_time;
    private float dash_time;
    public float punch_distance;

    private Transform target;
    private int direction;

    private bool is_dashing;
    private bool has_dashed;
    private bool has_punched;

    private Animator anim;
    private Rigidbody2D rb;

    private bool can_reset;

    //SFX
    AudioSource dash_sound;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        dash_sound = FindObjectOfType<SFXManager>().FindSFX("dash");

        dash_time = start_dash_time;
	}
	
	// Update is called once per frame
	void Update () {
		//if (dash_time <= 0)
  //      {
  //          if (!has_dashed)
  //          Dash();
  //      }
  //      else
  //      {
  //          dash_time -= Time.deltaTime;
  //      }

        if (is_dashing)
        {
            if (direction == 1)
            {
                if (transform.position.x >= target.position.x - punch_distance)
                {
                    if (!has_punched && is_dashing)
                    Punch();
                }
            }
            else if (direction == -1)
            {
                if (transform.position.x <= target.position.x + punch_distance)
                {
                    if (!has_punched && is_dashing)
                    Punch();
                }
            }
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BOSS_Dash_Recovery"))
        {
            can_reset = true;
        }
        //if (can_reset)
        //{
        //    GetComponent<BOSSInputManager>().ResetAttackTime();
        //    Debug.Log("ResetFromDash");
        //    can_reset = false;
        //}

        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("BOSS_Dash_Recovery"))
        {
            if (has_dashed)
            {
                has_dashed = false;
                //dash_time = start_dash_time;
                //GetComponent<BOSSInputManager>().ResetAttackTime();
                //Debug.Log("ResetFromDash");
                has_punched = false;
            }
        }

        
	}

   public void Dash()
    {
        if (FindObjectOfType<PlayerController>() != null)
            target = FindObjectOfType<PlayerController>().transform;

        dash_sound.Play();

        if (target.position.x > transform.position.x)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            direction = 1;
        }
        else if (target.position.x < transform.position.x)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            direction = -1;
        }
        anim.Play("BOSS_Dash_Active");

        is_dashing = true;
        has_dashed = true;
    }

    void Punch()
    {
        Debug.Log("Punch");

        is_dashing = false;
        has_punched = true;

        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.Play("BOSS_Dash_Punch");
    }
}
