using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
    public int damage_to_give;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "PlayerHealth")
        {
            other.gameObject.GetComponentInParent<PlayerHealthManager>().TakeDamage(damage_to_give);
            
            //Pick knockback direction
            if (other.gameObject.transform.position.x < transform.position.x)  //Knock left
            {
                other.gameObject.GetComponentInParent<Knockback>().DoKnockBack(0);
            }
            else if (other.gameObject.transform.position.x > transform.position.x)  //Knock right
            {
                other.gameObject.GetComponentInParent<Knockback>().DoKnockBack(1);
            }
        }
    }
   
}
