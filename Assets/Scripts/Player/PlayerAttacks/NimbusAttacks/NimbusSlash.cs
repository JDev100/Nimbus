using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusSlash : MonoBehaviour {
    private AudioSource slash_sound;

    private bool is_attacking;
    private Animator anim;

    //Only use when unlocked
    public bool unlocked;

    //For controlling input
    public bool can_input = true;
    // Use this for initialization
    void Start()
    {
        slash_sound = FindObjectOfType<SFXManager>().FindSFX("slash_sound");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {
            if (Input.GetButtonDown("Fire2") && !is_attacking && can_input)
            {
                //Decide whether air or ground attack
                if (GetComponent<PlayerJump>().is_grounded && !GetComponent<PlayerController>().crouching)
                {
                    anim.Play("Nimbus_Slash_Ground");
                }
                else if (GetComponent<PlayerJump>().is_grounded && GetComponent<PlayerController>().crouching)
                {
                    anim.Play("Nimbus_Crouch_Slash");
                }
                else if (!GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") >= 0)
                {
                    anim.Play("Nimbus_Slash_Air");
                }
                else if (!GetComponent<PlayerJump>().is_grounded && Input.GetAxisRaw("Vertical") < 0)
                {
                    anim.Play("Nimbus_Slash_Down");
                }


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
        }
    }
}
