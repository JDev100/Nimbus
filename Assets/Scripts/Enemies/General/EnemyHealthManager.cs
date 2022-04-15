using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
    public int enemy_max_health;
    public int enemy_current_health;

    public string death_sound_effect;
    private AudioSource death_sound;
    public bool boss = false;
    private bool die_once = false;

    //Visual FX
    public GameObject hit_effect;
    public GameObject death_effect;
    public Transform death_effect_pos;

	// Use this for initialization
	void Start () {
        //Set sound effects
        death_sound = FindObjectOfType<SFXManager>().FindSFX(death_sound_effect);

        enemy_current_health = enemy_max_health;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy_current_health <= 0)
        {
            //if (!boss)
            death_sound.Play();

            //Death effect
            //if (!boss)
            Instantiate(death_effect, death_effect_pos.transform.position, Quaternion.identity);

            //DropLoot
            if (!boss)
            FindObjectOfType<LootManager>().GetComponent<LootManager>().CalculateLoot(GetComponent<Transform>());

            GetComponent<EnemyStatsManager>().dead = true;

           // if(!boss)
            gameObject.SetActive(false);
            //else
            //{
            //    if  (!die_once)
            //    {
            //        Debug.Log("Died");
            //        // GetComponent<Animator>().SetBool("Death", true);
            //        GetComponent<Animator>().Play("BOSS_Death");
            //        die_once = true;
            //    }
                   
                
            //}
        }
	}

    public void TakeDamage(int damage_to_take)
    {
        Instantiate(hit_effect, transform.position, Quaternion.identity);
        enemy_current_health -= damage_to_take;

        //Shake camera
        FindObjectOfType<CameraController>().ShakeCamera();
    }
}
