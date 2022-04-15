using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSVineAttack : MonoBehaviour {
    public float start_time_between_attack;
    private float time_between_attack;

    public float attack_distance;

    private Transform target;

    private Animator anim;

    private bool attacking;

    private bool has_attacked;
	// Use this for initialization
	void Start () {
        time_between_attack = start_time_between_attack;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //target = FindObjectOfType<PlayerController>().transform;

        //if (Vector2.Distance(transform.position, target.position) < attack_distance)
        //{
        //    if (time_between_attack <= 0)
        //    {
        //        if (!attacking)
        //            VineAttack();

        //        time_between_attack = start_time_between_attack;
        //    }
        //    else
        //    {
        //        time_between_attack -= Time.deltaTime;
        //    }
        //}

        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BOSS_Vine_Attack"))
        //{
        //    attacking = true;
        //}
        //else
        //{
        //    attacking = false;
        //}
	}

    public void VineAttack()
    {
        anim.Play("BOSS_Vine_Attack");

        //GetComponent<BOSSInputManager>().ResetAttackTime();
        //Debug.Log("ResetFromVine");
    }
}
