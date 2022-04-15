using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSLightningAttack : MonoBehaviour {
    public float start_time_between_attack;
    private float time_between_attack;
    public float start_attack_buffer;
    private float attack_buffer;

    public int min_attack;
    public int max_attack;
    private int num_attacks;
    private int attack_index = 0;

    public GameObject lightningPrefab1;
    public GameObject lightningPrefab2;
    public Transform shoot_pos;

    private bool attacking;
    private bool has_attacked;

    private Animator anim;

    //SFX
    private AudioSource lightning_sound;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        lightning_sound = FindObjectOfType<SFXManager>().FindSFX("lightning_magic");
	}
	
	// Update is called once per frame
	void Update () {
        //if (time_between_attack <= 0f)
        //      {
        //          if (!has_attacked)
        //          {
        //              StartLightningAtttack();
        //          }
        //      }
        //      else
        //      {
        //          time_between_attack -= Time.deltaTime;
        //      }
        if (attack_index >= num_attacks && has_attacked)
        {
            //GetComponent<BOSSInputManager>().ResetAttackTime();
            //Debug.Log("ResetFromLightning");
            has_attacked = false;
        }
     
        if (attack_index >= num_attacks)
        {
            attacking = false;
            has_attacked = false;
            //time_between_attack = start_time_between_attack;
            attack_index = 0;
           
        
        }

        if (attacking)
        {
            if (attack_index <= num_attacks)
            {
                //if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("BOSS_Lightning_Attack"))
                //{
                //    LightningAttack();
                //}
                //Invoke("LightningAttack", attack_buffer);
                if (attack_buffer <= 0)
                {
                    LightningAttack();
                    attack_buffer = start_attack_buffer;
                }
                else
                {
                    attack_buffer -= Time.deltaTime;
                }
            }
        }
	}

    public void StartLightningAtttack()
    {
        has_attacked = true;
        attacking = true;
        num_attacks = Random.Range(min_attack, max_attack + 1);
        attack_index = 0;
    }

    private void LightningAttack()
    {
        if (attack_index == 0)
        anim.Play("BOSS_Lightning_Attack");
        StartCoroutine(AttackStartup());
        attack_index++;
    }

    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.25f);

        lightning_sound.Play();
        Instantiate(lightningPrefab1, shoot_pos.transform.position, Quaternion.identity);
        Instantiate(lightningPrefab2, shoot_pos.transform.position, Quaternion.identity);
    }
}
