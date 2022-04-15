using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusSlash : MonoBehaviour {
    private AudioSource slash_sound;
    public bool press_slash;
    public float start_attack_buffer;
    private float attack_buffer;
    private bool has_attacked;

    private bool is_attacking;
    private Animator anim;

  

    //For controlling input
    public bool can_input = true;
    // Use this for initialization
    void Start () {
        slash_sound = FindObjectOfType<SFXManager>().FindSFX("slash_sound");
        anim = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (attack_buffer <= 0)
        {
            if (!has_attacked)
            {
                press_slash = true;
                has_attacked = true;
            }
        }
        else
        {
            attack_buffer -= Time.deltaTime;
        }

        if (press_slash && !is_attacking && can_input)
        {
            //Decide whether air or ground attack
            if (GetComponentInParent<EvilNimbusJump>().is_grounded && !GetComponentInParent<EvilNimbusController>().crouching)
            {
                anim.Play("Nimbus_Slash_Ground");
            }
            else if (GetComponentInParent<EvilNimbusJump>().is_grounded && GetComponentInParent<EvilNimbusController>().crouching)
            {
                anim.Play("Nimbus_Crouch_Slash");
            }
            else if (!GetComponentInParent<EvilNimbusJump>().is_grounded)
            {
                anim.Play("Nimbus_Slash_Air");
            }

            //else if (!GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") < 0)
            //{
            //    anim.Play("Nimbus_Slash_Down");
            //}

            slash_sound.Play();
            is_attacking = true;
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Ground")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Air")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Crouch_Slash")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Down"))
        {
            is_attacking = true;
        }
        else
        {
            is_attacking = false;
        }
        if (has_attacked && press_slash)
        {
            press_slash = false;
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
