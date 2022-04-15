using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour {
    public int damage_to_give;

    public float start_hit_stun_time;

    public string hit_sound_effect;
    private AudioSource hit_sound;
    // Use this for initialization
    void Start () {
        hit_sound = FindObjectOfType<SFXManager>().FindSFX(hit_sound_effect);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHealth")
        {
            if (!other.gameObject.GetComponentInParent<EvilNimbusController>())
            {
                other.gameObject.GetComponentInParent<EnemyHealthManager>().TakeDamage(damage_to_give);
                other.gameObject.GetComponentInParent<EnemyStatsManager>().HitStun(start_hit_stun_time);
                other.gameObject.GetComponentInParent<EnemyStatsManager>().FlashRed();
                hit_sound.Play();
            }
            
        }
    }
}
