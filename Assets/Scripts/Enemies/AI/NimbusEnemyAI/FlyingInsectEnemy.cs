using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingInsectEnemy : MonoBehaviour {
    private float move_speed;

    public Transform start_pos;
    public Transform end_pos;

    private bool move_right = true;
    private bool turning;
    private bool has_turned;

    //For picking type
    public bool is_diver;

    //for Diving AI
    private bool diving = false;
    private int direction;
    private bool has_dived;
    private bool dive_active;
    private bool rise_active;
    public float dive_x_force;
    public float dive_y_force;
    public float start_dive_time;
    private float dive_time;
    private float rise_time;
    public float dive_distance;

    private Animator anim;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    

    // Update is called once per frame
    void Update () {
        //Raycast 
        

        if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < dive_distance
            && !diving && GameObject.FindGameObjectWithTag("Player").transform.position.y < transform.position.y && is_diver)
        {
            Debug.Log("Found Player");
            Dive();
        }
        //Diving AI
        if (diving)
        {
            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Dive_Active"))
            {
                dive_active = true;
            }
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Dive_Up_Start")
                && !rise_active && !dive_active)
            {
                rb.velocity = new Vector2(dive_x_force * direction, 0);
            }
            if (dive_time <= 0f && dive_active)
            {
                anim.SetTrigger("Up_Start");
                dive_active = false;
                rb.velocity = Vector2.zero;
                rise_time = start_dive_time;
            }
            else if (dive_time > 0f && dive_active)
            {
                dive_time -= Time.deltaTime;
                rb.velocity = new Vector2(dive_x_force * direction, -dive_y_force);
            }
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Dive_Up"))
            {
                rise_active = true;
                
            }
            if (rise_time <= 0f && rise_active)
            {
                anim.SetTrigger("Recovery");
                rise_active = false;
                rb.velocity = Vector2.zero;
            }
            else if (rise_time > 0f && rise_active)
            {
                rise_time -= Time.deltaTime;
                rb.velocity = new Vector2(dive_x_force * direction, dive_y_force);
            }
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Dive_Recovery"))
            {
                has_dived = false;
                diving = false;
            }
        }
        anim.SetBool("Diving", diving);

        if (turning)
        {
            move_speed = 0;
        }
        else
        {
            move_speed = GetComponent<EnemyStatsManager>().set_move_speed;
        }

            //Make sure enemy flies along correct path
            if (move_right && !turning && !diving)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

                transform.position = Vector2.MoveTowards(transform.position, end_pos.transform.position, move_speed * Time.deltaTime);
            }
            else if (!move_right && !turning && !diving)
            {

                transform.rotation = Quaternion.Euler(0, 180, 0);

                transform.position = Vector2.MoveTowards(transform.position, start_pos.transform.position, move_speed * Time.deltaTime);
            }

            //When enemy reaches one of the boundary positions
            if ((transform.position.x >= end_pos.transform.position.x || transform.position.x <= start_pos.transform.position.x) && !turning)
            {
                Turn();
                move_right = !move_right;
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Turn"))
            {
                has_turned = true;
            }
            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Insect_Turn") && has_turned)
            {
                turning = false;
                has_turned = false;
            }
        
	}

    private void Turn()
    {

        anim.SetTrigger("Turn");
        turning = true;
    }
    private void Dive()
    {
        Debug.Log("Diving");
        anim.SetTrigger("Dive");
        diving = true;
        dive_time = start_dive_time;
        //StartCoroutine(DiveBuffer());

        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    //IEnumerator DiveBuffer()
    //{
    //    yield return new WaitForSeconds(dive_buffer);
    //}
 }
