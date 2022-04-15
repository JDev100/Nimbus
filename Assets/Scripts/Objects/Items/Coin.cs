using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int money_to_give;
    public float y_force;
    public float max_x_force;
    private float x_force;

    //SFX
    private AudioSource get_coin_sound;
    private AudioSource coin_bounce;
	// Use this for initialization
	void Start () {
        get_coin_sound = FindObjectOfType<SFXManager>().FindSFX("get_coin");
        coin_bounce = FindObjectOfType<SFXManager>().FindSFX("coin_bounce");

        x_force = Random.Range(0, max_x_force);

        int direction = Random.Range(0, 2);
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(x_force, y_force));
        }
        else if (direction == 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-x_force, y_force));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            get_coin_sound.Play();
            other.gameObject.GetComponent<PlayerMoneyManager>().AddCurrency(money_to_give);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

        if (other.gameObject.tag == "LevelCollision")
        {
            if (GetComponent<Rigidbody2D>().velocity.y > 5 || GetComponent<Rigidbody2D>().velocity.y < -5)
            coin_bounce.Play();
        }
    }
}
