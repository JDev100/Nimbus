using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusFowardMagic : MonoBehaviour {
    public GameObject projectile;
    public Transform shoot_pos;
    private bool is_attacking;

    private Animator anim;

    //Only use when unlocked
    public bool unlocked = false;

    // Initializes magic cost for attack
    public int magicCost;

    //References the Magic Manager
    private PlayerMagicManager playerMagic;

    //For controlling input
    public bool can_input = true;
    private bool has_magic;

    //SFX
    private AudioSource fire_magic_sound;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        fire_magic_sound = FindObjectOfType<SFXManager>().FindSFX("fire_magic");

        //Initializes Magic Manager
        playerMagic = GetComponent<PlayerMagicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {

            if (Input.GetButtonDown("Fire1") && !is_attacking && can_input && has_magic)
            {
                anim.Play("Nimbus_Magic_Forward");
                fire_magic_sound.Play();
                StartCoroutine(AttackStartup());

                //Use up magic
                playerMagic.currentMagic -= magicCost;
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Magic_Forward"))
            {
                is_attacking = true;
            }
            else
            {
                is_attacking = false;
            }

            // Prevents player from using spell without magic
            if (playerMagic.currentMagic <= 0)
            {

                has_magic = false;
            }
            else
            {
                has_magic = true;
            }
        }
    }

    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.1176470588f);

        Instantiate(projectile, shoot_pos.transform.position, Quaternion.identity);
    }
}
