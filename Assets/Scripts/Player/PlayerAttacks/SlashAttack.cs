using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    private AudioSource slash_sound;

    private bool is_attacking;
    private Animator anim;

    //For controlling input
    public bool can_input = true;
    // Use this for initialization
    void Start()
    {
        //Set sound effect
        slash_sound = FindObjectOfType<SFXManager>().FindSFX("slash_sound");

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Period) && !is_attacking && can_input)
        {
            anim.Play("Player_Attack");


            slash_sound.Play();
            is_attacking = true;
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            is_attacking = true;
        }
        else
        {
            is_attacking = false;
        }
    }
}
