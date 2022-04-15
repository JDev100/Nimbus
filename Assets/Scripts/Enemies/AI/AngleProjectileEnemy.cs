using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleProjectileEnemy : MonoBehaviour {
    //For Movement
    public float move_speed;
    public float start_move_timer;
    private float move_timer;
    private int move_direction;
    public float follow_distance;
    

    //For Projectile
    public float start_throw_timer;
    private float throw_timer;
    public GameObject projectile;
    public Transform shoot_pos;
    public int direction;

    //For keeping enemy on platform
    public Transform wall_check;
    public float wall_check_radius;
    public LayerMask what_is_wall;
    private bool hit_wall;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        hit_wall = Physics2D.OverlapCircle(wall_check.transform.position, wall_check_radius, what_is_wall);
    }

    // Update is called once per frame
    void Update () {
        //Update the move speed
        move_speed = GetComponent<EnemyStatsManager>().set_move_speed;

        //Controls which way enemy is facing
        if (transform.position.x > FindObjectOfType<PlayerController>().gameObject.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = 0;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = 1;
        }
            
        //Timer for controlling when to throw projectile
		if (throw_timer <= 0)
        {
            rb.velocity = Vector2.zero;
            Instantiate(projectile, shoot_pos.position, Quaternion.identity);
            throw_timer = start_throw_timer;
        }
        else
        {
            throw_timer -= Time.deltaTime;
        }

        //Timer for controlling movement
        if (move_timer <= 0)
        {
            move_direction = Random.Range(0, 2);
            move_timer = start_move_timer;
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

        //Keep Enemy on Platform
        if (hit_wall)
        {
          //  rb.velocity = Vector2.zero;
        }
            
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
