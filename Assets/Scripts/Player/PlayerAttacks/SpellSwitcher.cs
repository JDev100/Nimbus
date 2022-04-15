using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSwitcher : MonoBehaviour
{
    private int[] spells;
    private int size = 4;

    


    private NimbusFowardMagic projScript;
    private ArcFire arcScript;
    private HolyFire holyScript;
    private ExplodingFire explodeScript;


    private AudioSource spell_scroll;
            
    // Start is called before the first frame update
    void Start()
    {
        spell_scroll = FindObjectOfType<SFXManager>().FindSFX("spell_scroll");

        spells = new int[size];


        projScript = GetComponent<NimbusFowardMagic>();
        arcScript = GetComponent<ArcFire>();
        holyScript = GetComponent<HolyFire>();
        explodeScript = GetComponent<ExplodingFire>();




    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("R1") && !explodeScript.have_shot)
        {
            size += 1;
            spell_scroll.Play();
            FindObjectOfType<PlayerHUDManager>().spell_display.GetComponent<Animator>().Play("SpellDisplayScrollNext");
        }

        if (Input.GetButtonDown("L1") && !explodeScript.have_shot)
        {
            size -= 1;
            spell_scroll.Play();
            FindObjectOfType<PlayerHUDManager>().spell_display.GetComponent<Animator>().Play("SpellDisplayScrollPrevious");
        }

        if (size > 3)
        {
            size = 0;
        }

        if (size < 0)
        {
            size = 3;
        }

        if (size == 0)
        {
            FindObjectOfType<PlayerHUDManager>().spell_display.sprite = FindObjectOfType<PlayerHUDManager>().FindSpell("fire").sprite;

            projScript.enabled = true;
            arcScript.enabled = false;
            holyScript.enabled = false;
            explodeScript.enabled = false;

   
        }

        if (size == 1)
        {
            FindObjectOfType<PlayerHUDManager>().spell_display.sprite = FindObjectOfType<PlayerHUDManager>().FindSpell("lightning").sprite;

            projScript.enabled = false;
            arcScript.enabled = true;
            holyScript.enabled = false;
            explodeScript.enabled = false;

         
        }

        if (size == 2)
        {

            FindObjectOfType<PlayerHUDManager>().spell_display.sprite = FindObjectOfType<PlayerHUDManager>().FindSpell("ice").sprite;

            projScript.enabled = false;
            arcScript.enabled = false;
            holyScript.enabled = true;
            explodeScript.enabled = false;

        }

        if (size == 3)
        {
            FindObjectOfType<PlayerHUDManager>().spell_display.sprite = FindObjectOfType<PlayerHUDManager>().FindSpell("plasma").sprite;

            projScript.enabled = false;
            arcScript.enabled = false;
            holyScript.enabled = false;
            explodeScript.enabled = true;

        }      
    }
}
