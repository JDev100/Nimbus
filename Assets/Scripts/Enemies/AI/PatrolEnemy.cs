using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour {
    public float move_speed;
    private bool move_right = true;

    public Transform wall_check;
    public float wall_check_radius;
    public LayerMask what_is_wall;
    private bool hit_wall;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        
	}

    private void FixedUpdate()
    {
        hit_wall = Physics2D.OverlapCircle(wall_check.transform.position, wall_check_radius, what_is_wall);

        //if (hit_wall)
        //    move_right = !move_right;
    }

    // Update is called once per frame
    void Update () {
        move_speed = GetComponent<EnemyStatsManager>().set_move_speed;

        if (hit_wall)
            move_right = !move_right;

        if (move_right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2(move_speed, rb.velocity.y);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2(-move_speed, rb.velocity.y);
        }
	}
    
}
