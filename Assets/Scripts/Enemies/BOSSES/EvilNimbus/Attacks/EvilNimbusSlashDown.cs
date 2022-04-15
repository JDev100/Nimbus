using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusSlashDown : MonoBehaviour {
    private bool is_attacking;

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Slash_Down"))
        {
            is_attacking = true;
        }
        else
        {
            is_attacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !is_attacking && !GetComponentInParent<EvilNimbusJump>().is_grounded)
        {
            GetComponentInParent<Animator>().Play("Nimbus_Slash_Down");
        }
    }
}
