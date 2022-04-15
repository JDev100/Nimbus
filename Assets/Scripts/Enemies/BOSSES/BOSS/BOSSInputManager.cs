using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSInputManager : MonoBehaviour {
    public float min_attack_time;
    public float max_attack_time;
    public float attack_time;
    public bool standby = true;

    public float vine_attack_distance;
    public float dash_distance;
    public float chase_distance;

    private int attack_index;
    private bool has_attacked;

    private Transform target;

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        attack_index = 0;
	}
	
	// Update is called once per frame
	void Update () {
        target = FindObjectOfType<PlayerController>().transform;

        if (!standby)
        {



            //Set proper rotation
            if (target.position.x > transform.position.x)
            {
                transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (target.position.x < transform.position.x)
            {
                transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (attack_time <= 0)
            {
                if (!has_attacked)
                {
                    if (Vector2.Distance(transform.position, target.transform.position) < vine_attack_distance)
                    {
                        attack_index = Random.Range(1, 4);
                    }
                    else if (Vector2.Distance(transform.position, target.transform.position) > vine_attack_distance
                        && Vector2.Distance(transform.position, target.transform.position) < dash_distance)
                    {
                        attack_index = Random.Range(2, 4);
                    }
                    else if (Vector2.Distance(transform.position, target.transform.position) >= dash_distance)
                    {
                        attack_index = 3;
                    }
                }

                //attack_time = Random.Range(min_attack_time, max_attack_time);
            }
            else
            {
                attack_time -= Time.deltaTime;
            }

            if (attack_index == 1)
            {
                GetComponent<BOSSVineAttack>().VineAttack();
                attack_index = 0;
                has_attacked = true;
            }
            else if (attack_index == 2)
            {
                GetComponent<BOSSLightningAttack>().StartLightningAtttack();
                attack_index = 0;
                has_attacked = true;
            }
            else if (attack_index == 3)
            {
                GetComponent<BOSSDash>().Dash();
                attack_index = 0;
                has_attacked = true;
            }
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BOSS_Idle") && has_attacked)
            {
                ResetAttackTime();
                has_attacked = false;
            }
        }
    }

    public void ResetAttackTime()
    {
        has_attacked = false;
        attack_time = Random.Range(min_attack_time, max_attack_time);
        
    }
}
