using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour {
    public string animtrigger;
    public float start_attack_buffer;
    private bool trigger;
    private float attack_buffer;
    private bool has_attacked;

    private bool is_attacking;
    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (attack_buffer <= 0)
        {
            if (!has_attacked)
            {
                trigger = true;
                has_attacked = true;
            }
        }
        else
        {
            trigger = false;
            attack_buffer -= Time.deltaTime;
        }

        if (trigger && !is_attacking)
        {
            //Decide whether air or ground attack
            anim.Play(animtrigger);

            //else if (!GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") < 0)
            //{
            //    anim.Play("Nimbus_Slash_Down");
            //}

            //slash_sound.Play();
            is_attacking = true;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName(animtrigger))
        {
            is_attacking = true;
        }
        else
        {
            is_attacking = false;
        }
        if (has_attacked && trigger)
        {
            trigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack_buffer = start_attack_buffer;
            has_attacked = false;
        }
    }
}
