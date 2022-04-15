using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingFire : MonoBehaviour
{
    public GameObject projectile;
    public GameObject explosion;
    public Transform shoot_pos;
    public Transform projectilePoint;

    public bool unlocked = false;

    //check for if already shot
    public bool have_shot;

    //input for mana management
    public bool hasMagic;

    // Initializes magic cost for attack
    public int magicCost;

    //References the Magic Manager
    public PlayerMagicManager playerMagic;


    private Animator anim;

    //SFX
    private AudioSource plasma_shoot;
    private AudioSource plasma_detonate;
   
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        //SFX
        plasma_shoot = FindObjectOfType<SFXManager>().FindSFX("plasma_shoot");
        plasma_detonate = FindObjectOfType<SFXManager>().FindSFX("plasma_detonate");

        //Initializes Magic Manager
        playerMagic = GetComponent<PlayerMagicManager>();
  

    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {



            if (Input.GetButtonDown("Fire1"))
            {
                if (!have_shot && hasMagic)
                {
                    anim.Play("Nimbus_Magic_Forward");
                    StartCoroutine(AttackStartup());
                    have_shot = true;
                    playerMagic.currentMagic -= magicCost;
                    Debug.Log("Shot");
                }

                else
                {
                    if (projectilePoint != null && FindObjectOfType<PlayerProjectile>() != null)
                    {
                        projectilePoint.position = FindObjectOfType<PlayerProjectile>().transform.position;
                        Instantiate(explosion, projectilePoint.position, Quaternion.identity);
                        plasma_detonate.Play();
                        Debug.Log("Exploded");
                    }
                   
                    have_shot = false;
                }
            }

            if (playerMagic.currentMagic > 0)
            {
                hasMagic = true;
            }

            else if (playerMagic.currentMagic <= 0)
            {
                hasMagic = false;
            }






        }
    }
    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.1176470588f);

        Instantiate(projectile, shoot_pos.transform.position, Quaternion.identity);
        plasma_shoot.Play();
    }
}

