using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    public int health_to_raise;

    //SFX
    private AudioSource get_health;
	// Use this for initialization
	void Start () {
        get_health = FindObjectOfType<SFXManager>().FindSFX("get_health");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHealth")
        {
            get_health.Play();
            other.gameObject.GetComponentInParent<PlayerHealthManager>().RaiseHealth(health_to_raise);
            Destroy(gameObject);
        }
    }
}
