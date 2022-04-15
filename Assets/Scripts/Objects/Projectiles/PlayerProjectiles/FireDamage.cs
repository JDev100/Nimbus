using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour {
    public int damage_to_give;

    public float start_hit_stun_time;

    //Time the fire exists
    public float existTime;

    public GameObject floorFire;

    //timer between damage
    private float damageTimer;
    public float determinedTimer;

    //SFX
    private AudioSource ice_impact;

    void FixedUpdate()
    {
        ice_impact = FindObjectOfType<SFXManager>().FindSFX("ice_impact");

        if (damageTimer >= determinedTimer)
        {
            damageTimer = 0;
        }
        damageTimer += Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update ()
    {
        existTime -= Time.deltaTime;      
        
        if (existTime <= 0)
        {
            Destroy(floorFire);
        }


	}


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHealth")
        {
            if (damageTimer >= determinedTimer)
            {
                Debug.Log("Burning Enemy");
                other.gameObject.GetComponentInParent<EnemyHealthManager>().TakeDamage(damage_to_give);
                other.gameObject.GetComponentInParent<EnemyStatsManager>().HitStun(start_hit_stun_time);
                other.gameObject.GetComponentInParent<EnemyStatsManager>().FlashRed();
                ice_impact.Play();
            }
        }
    }


}
