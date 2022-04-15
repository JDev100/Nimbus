using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItem : MonoBehaviour {
    public int magic_to_raise;

    //SFX
    private AudioSource get_magic;
	// Use this for initialization
	void Start () {
        get_magic = FindObjectOfType<SFXManager>().FindSFX("get_health");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHealth")
        {
            get_magic.Play();
            other.gameObject.GetComponentInParent<PlayerMagicManager>().RaiseMagic(magic_to_raise);
            Destroy(gameObject);
        }
    }
}
