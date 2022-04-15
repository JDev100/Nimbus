using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloEnemy : MonoBehaviour {
    private float move_speed;
    public float start_time_between_attack;
    private float time_between_attack;

    private bool move_right = false;

    private Rigidbody2D rb;
    private Animator anim;

    //For roll AI
    private bool rolling = false;
    public float roll_speed;
    RaycastHit2D hit_info;
    public LayerMask what_is_player;
    public float distance;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        Physics2D.queriesStartInColliders = false;
    }
	
	// Update is called once per frame
	void Update () {
        move_speed = GetComponent<EnemyStatsManager>().set_move_speed;

        if (rolling)
        {
            if (move_right)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = new Vector2(roll_speed, rb.velocity.y);
            }

            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.velocity = new Vector2(-roll_speed, rb.velocity.y);
            }
        }
        else
        {
            if (move_right)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = new Vector2(move_speed, rb.velocity.y);
            }
            else if (!move_right)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.velocity = new Vector2(-move_speed, rb.velocity.y);
            }
        }

       

        if (rb.velocity.x > 0)
        {
            hit_info = Physics2D.Raycast(transform.position, Vector2.right, distance, what_is_player);
        }
        else if (rb.velocity.x < 0)
        {
            hit_info = Physics2D.Raycast(transform.position, Vector2.left, distance, what_is_player);
        }

        if (time_between_attack <= 0)
        {
            if (hit_info.collider != null)
            {
                if (hit_info.collider.CompareTag("PlayerHealth"))
                {
                    time_between_attack = start_time_between_attack;
                }
            }
            rolling = false;
        }
        else
        {
            rolling = true;
          
            
            time_between_attack -= Time.deltaTime;
        }



        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Armadillo_Enemy_Roll_Start")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Armadillo_Enemy_Roll_Recovery"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        anim.SetBool("Rolling", rolling);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBoundary")
        {
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                move_right = false;
            }
            else if (other.gameObject.transform.position.x < transform.position.x)
            {
                move_right = true;
            }
        }
    }
}
