using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour {
    private float move_speed;
    private bool move_right = true;
    private int direction;
    public float distance;

    public float start_time_between_attack;
    private float time_between_attack;
    public float start_time_for_attack;
    private float time_for_attack;
    private bool is_attacking;
    private bool can_shoot;
    private bool has_attacked;

    public bool is_shooter;
    //For Shoot AI
    public GameObject projectile;
    //For projectile attack
    RaycastHit2D hit_info;
    public Transform shoot_pos;
    public LayerMask what_is_player;

    private Rigidbody2D rb;
    private Animator anim;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Physics2D.queriesStartInColliders = false;
	}
	
	// Update is called once per frame
	void Update () {
        move_speed = GetComponent<EnemyStatsManager>().set_move_speed;
        if (move_right)
        {
            direction = 1;
           
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2(move_speed, rb.velocity.y);
        }
        else
        {
            direction = -1;
            
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2(-move_speed, rb.velocity.y);
        }

     //  hit_info = Physics2D.Raycast(transform.position, Vector2.right, distance, what_is_player);
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
                if (hit_info.collider.CompareTag("PlayerHealth") && !is_attacking & !has_attacked && is_shooter)
                {
                    anim.Play("Mushroom_Enemy_Attack");
                    StartCoroutine(AttackStartup());
                    time_for_attack = start_time_for_attack;
                    has_attacked = true;
                    time_between_attack = start_time_between_attack;
                }
            }
        }
        else
        {
            time_between_attack -= Time.deltaTime;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Mushroom_Enemy_Attack"))
        {
            is_attacking = true;
        }
        else
        {
            is_attacking = false;
        }
        //Raycast in proper direction


        //The AI for patrolling

        if (has_attacked)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (time_for_attack <= 0)
            {
                has_attacked = false;
            }
            else
            {
                time_for_attack -= Time.deltaTime;
            }
        }
    }
    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.4090909091f);

        Instantiate(projectile, shoot_pos.transform.position, Quaternion.identity);
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
