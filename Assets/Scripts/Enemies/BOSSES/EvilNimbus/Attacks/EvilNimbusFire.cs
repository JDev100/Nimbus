using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusFire : MonoBehaviour {
    public GameObject projectile;
    public Transform shoot_pos;
    public bool press_fire;
    private bool is_attacking;

    private Animator anim;
    
    //For controlling input
    public bool can_input = true;

    //SFX
    private AudioSource fire_magic_sound;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        fire_magic_sound = FindObjectOfType<SFXManager>().FindSFX("fire_magic");
        
    }

    // Update is called once per frame
    void Update()
    {
       
            if (press_fire && !is_attacking && can_input)
            {
                anim.Play("Nimbus_Magic_Forward");
                fire_magic_sound.Play();
                StartCoroutine(AttackStartup());
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Magic_Forward"))
            {
                is_attacking = true;
            }
            else
            {
                is_attacking = false;
            }
    }

    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.1176470588f);

        Instantiate(projectile, shoot_pos.transform.position, Quaternion.identity);
    }
}
