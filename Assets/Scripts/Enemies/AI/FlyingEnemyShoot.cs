using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyShoot : MonoBehaviour {

    public float shoot_distance;

    private Transform player;
    public float start_shoot_time;
    private float shoot_time;

    //Can only shoot when at a certain distance from the player
    private bool can_shoot;

    //What to shoot
    public GameObject projectile_prefab;
    public Transform shoot_pos;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Vector2.Distance(transform.position, player.position) < shoot_distance)
        {
            can_shoot = true;
            if (can_shoot)
            {
                if (shoot_time <= 0)
                {
                    Instantiate(projectile_prefab, shoot_pos.transform.position, Quaternion.identity);
                    shoot_time = start_shoot_time;
                }
                else
                {
                    shoot_time -= Time.deltaTime;
                }
            }
        }
        else
        {
            can_shoot = false;
        }
	}
    
}
