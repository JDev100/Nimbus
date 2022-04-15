using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBallDamage : MonoBehaviour
{
    public int damage_to_give;

    public float start_hit_stun_time;

    private ExplodingFire fire;

    void Start()
    {
        fire = FindObjectOfType<ExplodingFire>().GetComponent<ExplodingFire>();    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHealth" && !other.gameObject.GetComponentInParent<EvilNimbusController>())
        {
            other.gameObject.GetComponentInParent<EnemyHealthManager>().TakeDamage(damage_to_give);
            other.gameObject.GetComponentInParent<EnemyStatsManager>().HitStun(start_hit_stun_time);
            other.gameObject.GetComponentInParent<EnemyStatsManager>().FlashRed();
            fire.have_shot = false;
        }

        if (other.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
