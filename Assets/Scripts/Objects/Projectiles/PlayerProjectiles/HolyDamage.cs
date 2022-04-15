using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyDamage : MonoBehaviour {
    public int damage_to_give;

    public GameObject holyBall;
    public GameObject floorFire;
    public Transform collidePoint;

    public float start_hit_stun_time;

    //SFX
    private AudioSource ice_magic_sound;
    private AudioSource ice_impact_sound;

    private void Start()
    {
        ice_magic_sound = FindObjectOfType<SFXManager>().FindSFX("ice_magic");
        ice_impact_sound = FindObjectOfType<SFXManager>().FindSFX("ice_impact");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if (other.gameObject.tag == "EnemyHealth")
        //{
        //    ice_impact_sound.Play();
        //    other.gameObject.GetComponentInParent<EnemyHealthManager>().TakeDamage(damage_to_give);
        //    other.gameObject.GetComponentInParent<EnemyStatsManager>().HitStun(start_hit_stun_time);
        //    other.gameObject.GetComponentInParent<EnemyStatsManager>().FlashRed();
        //    Destroy(holyBall);
        //}

        if (other.gameObject.tag == "LevelCollision")
        {
            Debug.Log("Collided with level");
            ice_magic_sound.Play();
            Instantiate(floorFire, collidePoint.position, collidePoint.rotation);
            Destroy(holyBall);
        }


    
    
    }

}
