using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : MonoBehaviour {
    public float move_speed;
    public float chase_distance;
    public float hit_distance;
    public float start_move_timer;
    private float move_timer;
    private int move_direction;

    public bool is_attacking;
    private bool do_once;
    private bool is_chasing;

    //For keeping enemy on platform
    public Transform wall_check;
    public float wall_check_radius;
    public LayerMask what_is_wall;
    private bool hit_wall;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        anim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        hit_wall = Physics2D.OverlapCircle(wall_check.transform.position, wall_check_radius, what_is_wall);
    }

    // Update is called once per frame
    void Update () {
        move_speed = GetComponent<EnemyStatsManager>().set_move_speed;

        //Controls which way enemy is facing
        if (transform.position.x > FindObjectOfType<PlayerController>().gameObject.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //What to do when enemy is attacking
        if (is_attacking)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Slash_Enemy_Attack");
            StartCoroutine(AttackTimer());
        }
        //Other actions when not attacking
        else
        {
            do_once = true;
            //When player is not in range
            if (Vector2.Distance(transform.position, player.transform.position) > chase_distance)
            {
                //Timer for controlling movement
                if (move_timer <= 0)
                {
                    move_direction = Random.Range(0, 2);
                    move_timer = start_move_timer;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    move_timer -= Time.deltaTime;
                    if (move_direction == 0)
                    {
                        rb.velocity = new Vector2(-move_speed, rb.velocity.y);
                    }
                    else if (move_direction == 1)
                    {
                        rb.velocity = new Vector2(move_speed, rb.velocity.y);
                    }
                }
            }
            //When Player is in range
            else if (Vector2.Distance(transform.position, player.transform.position) < chase_distance)
            {
                if (do_once)
                {
                    rb.velocity = Vector2.zero;
                    do_once = false;
                }
               
                //Move towards player
                if (transform.position.x > player.transform.position.x)
                {
                    rb.velocity = new Vector2(-move_speed, rb.velocity.y);
                }
                else if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(move_speed, rb.velocity.y);
                }

                //If player is in hit distance
                if (Vector2.Distance(transform.position, player.transform.position) < hit_distance)
                {
                    is_attacking = true;
                }
            }
        }
	}

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1.8f);
        is_attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBoundary")
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                move_direction = 3;
                rb.velocity = new Vector2(move_speed, rb.velocity.y);
            }
            else
            {
                move_direction = 3;
                rb.velocity = new Vector2(-move_speed, rb.velocity.y);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBoundary")
        {
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                move_direction = 3;
                rb.velocity = new Vector2(move_speed, rb.velocity.y);
            }
            else
            {
                move_direction = 3;
                rb.velocity = new Vector2(-move_speed, rb.velocity.y);
            }
        }
    }
}
