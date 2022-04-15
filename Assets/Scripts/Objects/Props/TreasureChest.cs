using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {
    public GameObject treasure_prefab;
   

    private bool is_open;

    private bool has_been_opened;

    //SFX stuff
    private AudioSource open_chest_sound;
	// Use this for initialization
	void Start () {
        //Set up SFX
    
        open_chest_sound = FindObjectOfType<SFXManager>().FindSFX("open_chest");
	}
	
	// Update is called once per frame
	void Update () {
		if (is_open && !has_been_opened)
        {
            Instantiate(treasure_prefab, transform.position, Quaternion.identity);

            has_been_opened = true;
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHealth")
        {
            if (Input.GetAxisRaw("Vertical") > 0.5f && !is_open)
            {
                //Play SFX
                open_chest_sound.Play();

                is_open = true;
                GetComponent<Animator>().SetBool("IsOpen", true);
                GetComponent<Animator>().SetBool("IsClosed", false);
            }
        }
    }
}
