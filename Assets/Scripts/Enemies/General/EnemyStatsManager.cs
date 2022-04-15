using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour {
    public float set_move_speed;
    private float constant_move_speed;

    private SpriteRenderer sprite;
    private Color originalColor;
    public float flash_time;
    private float stun_time;

    public bool can_stun = true;

    //For checking if enemy is dead from other scripts
    public bool dead;

    //For respawn stuff
    public bool activePrimed = false;
	// Use this for initialization
	void Start () {

        //For use in enemy telegraph
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        constant_move_speed = set_move_speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (stun_time <= 0)
        {
            set_move_speed = constant_move_speed;
        }
        else
        {
            stun_time -= Time.deltaTime;
            set_move_speed = 0;

            if(GetComponent<Rigidbody2D>() != null && can_stun)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        ////For checking if current enemy is dead
        //if (GetComponent<EnemyHealthManager>().enemy_current_health <= 0f)
        //{
        //    dead = true;
        //}
        //else
        //{
        //    dead = false;
        //}
	}

    public void HitStun(float time)
    {
        stun_time = time;
    }

    public void FlashRed()
    {
        if (GetComponent<EnemyHealthManager>().enemy_current_health > 0)

        sprite.color = Color.red;
        Invoke("ResetColor", flash_time);
    }
    void ResetColor()
    {
        sprite.color = originalColor;
    }
}
