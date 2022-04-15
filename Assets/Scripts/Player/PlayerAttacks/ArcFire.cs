using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcFire : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public int magicCost;
    public PlayerMagicManager playerMagic;
    private bool canCast;

    public bool can_input;
    private bool attacking;

    private Animator anim;

    public bool unlocked = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMagic = GetComponent<PlayerMagicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {

            if (Input.GetButtonDown("Fire1") && canCast && !attacking)
            {
                anim.Play("Nimbus_Magic_Arc");
                StartCoroutine(AttackStartup());
                playerMagic.currentMagic -= magicCost;
            }

            if (playerMagic.currentMagic <= 0)
            {
                canCast = false;

                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("No more magic");
                }
            }
            else
            {
                canCast = true;
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Magic_Arc"))
            {
                attacking = true;
            }
            else
            {
                attacking = false;
            }

        }
    }


    void Shoot()
    {
        Debug.Log("It shoot good");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(0.1176470588f);

        Shoot();
    }
}
