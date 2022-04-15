// This manages the player's magic resources, and all spells will call this script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMagicManager : MonoBehaviour
{
    public float currentMagic;
    public int maxMagic;

    //For displaying magic effect when getting magic
    public GameObject magic_effect;

    public float deplete_speed;
    // Start is called before the first frame update
    void Start()
    {
        currentMagic = maxMagic;
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.color = Color.white;
        //changes the health bar and text based on player's current health
        if (FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount > currentMagic / maxMagic)
        {
            FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount = currentMagic / maxMagic;
        }
        if (FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount < currentMagic / maxMagic)
        {
            FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount = currentMagic / maxMagic;
        }
        if (FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount < FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount)
        {

            FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount += deplete_speed * Time.deltaTime;
        }

        if (FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount >
            FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount = FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount;
        }

        if (FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount >
            FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount -= deplete_speed * Time.deltaTime;
        }
        if (FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount <
            FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.fillAmount = FindObjectOfType<PlayerHUDManager>().magic_bar.bar.fillAmount;
        }
        if (currentMagic <= 0)
        {
            currentMagic = 0;
        }

        //Make sure player doesnt go over max magic
        if (currentMagic > maxMagic)
        {
            currentMagic = maxMagic;
        }
    }

    //Raise magic
    public void RaiseMagic (int magic_to_raise)
    {
        FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.color = Color.cyan;

        currentMagic += magic_to_raise;

        Instantiate(magic_effect, transform.position, Quaternion.identity);
    }

    public void UpgradeMagic(int magic_to_upgrade)
    {
        FindObjectOfType<PlayerHUDManager>().magic_bar.follow_bar.color = Color.cyan;

        maxMagic += magic_to_upgrade;
        currentMagic = maxMagic;
    }
}
