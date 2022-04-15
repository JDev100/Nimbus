using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour {
    public GameObject projectile;
    public Transform shoot_pos;
    private bool is_attacking;

    private Animator anim;

    // Initializes magic cost for attack
    public int magicCost;

    //References the Magic Manager
    private PlayerMagicManager playerMagic;

    //For controlling input
    public bool can_input = true;
    private bool has_magic;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        //Initializes Magic Manager
        playerMagic = GetComponent<PlayerMagicManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Comma) && !is_attacking && can_input && has_magic)
        {
            anim.Play("Player_Magic");
            StartCoroutine(AttackStartup());

            //Use up magic
            playerMagic.currentMagic -= magicCost;
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Magic"))
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
    
    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.58333f);

        Instantiate(projectile, shoot_pos.transform.position, Quaternion.identity);
    }
}
