using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public int magicCost;
    public PlayerMagicManager playerMagic;
    private bool canCast;

    //Only use when unlocked
    public bool unlocked = false;
    private bool attacking;

    private Animator anim;
    // Start is called before the first frame update
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
                anim.Play("Nimbus_Magic_Ice");
                StartCoroutine(AttackStartup());
                playerMagic.currentMagic -= magicCost;
            }

            if (playerMagic.currentMagic <= 0)
            {
                canCast = false;

                if (Input.GetButtonDown("Fire2"))
                {
                    Debug.Log("No more magic");
                }
            }
            else
            {
                canCast = true;
            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Nimbus_Magic_Ice"))
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
