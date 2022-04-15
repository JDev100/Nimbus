using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour {
    public GameObject prefab;
    private bool hit = false;
	// Use this for initialization
	void Start () {
        prefab.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (hit)
        {
            prefab.SetActive(true);
        }
        else
        {
            prefab.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon" || other.gameObject.tag == "Explosion")
        {
            hit = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
