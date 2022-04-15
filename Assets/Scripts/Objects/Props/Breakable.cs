using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    public int number_of_loot;
    public GameObject loot;
    public GameObject hit_effect;

    public bool is_random = false;

    private AudioSource break_sound;
    private Color original_color;
    private bool broken;

    public float respawn_time;
    private float respawn_coundown;
	// Use this for initialization
	void Start () {
        original_color = GetComponent<SpriteRenderer>().color;
        break_sound = FindObjectOfType<SFXManager>().FindSFX("breakable");
	}
	
	// Update is called once per frame
	void Update () {
		if (broken)
        {
            if (respawn_coundown <= 0)
            {
                if (!GetComponent<Renderer>().isVisible)
                {
                    GetComponent<SpriteRenderer>().color = original_color;
                    GetComponent<BoxCollider2D>().enabled = true;
                    broken = false;
                }
            }
           else
            {
                respawn_coundown -= Time.deltaTime;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            BreakObject();
        }
    }

    private void BreakObject()
    {
        Instantiate(hit_effect, transform.position, Quaternion.identity);
        break_sound.Play();
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        GetComponent<BoxCollider2D>().enabled = false;

        if (is_random)
        {
            FindObjectOfType<LootManager>().CalculateLoot(GetComponent<Transform>(), 100);
        }
        else
        {
            for (int i = 0; i < number_of_loot; i++)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
        broken = true;
        respawn_coundown = respawn_time;
    }
}
