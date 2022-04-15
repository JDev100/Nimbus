using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusProjectile : MonoBehaviour {
    public float force;
    private bool shoot_right;

    //For SFX
    public string explosion_sound_effect;
    private AudioSource explosion_sound;
    public GameObject hit_effect;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        //Set SFX
        explosion_sound = FindObjectOfType<SFXManager>().FindSFX(explosion_sound_effect);

        rb = GetComponent<Rigidbody2D>();
        shoot_right = FindObjectOfType<EvilNimbusController>().is_facing_right;

        if (shoot_right)
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2(force, 0);
        }
        else
        {
            transform.rotation = transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2(-force, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
