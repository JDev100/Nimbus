using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNimbusHealth : MonoBehaviour {
    public int max_health;
    public float current_health;
    public float start_invincibility_frames;
    private float invincibility_frames;
    private bool can_get_hit;

    public GameObject damage_collision;

    private AudioSource take_damage;
    private AudioSource hit_sound;
    public GameObject hit_effect;
    private SpriteRenderer sprite;
    private Color originalColor;
    public float flash_time;
    private float stun_time;

    public GameObject weapontrigger1;
    public GameObject weapontrigger2;

    private bool dead = false;
    // Use this for initialization
    void Start () {
        sprite = GetComponentInParent<SpriteRenderer>();
        originalColor = sprite.color;

        current_health = max_health;

        take_damage = FindObjectOfType<SFXManager>().FindSFX("take_damage");
        hit_sound = FindObjectOfType<SFXManager>().FindSFX("hit_sound");
    }
	
	// Update is called once per frame
	void Update () {
        {
            if (current_health <= 0)
            {
                GetComponentInParent<EvilNimbusInputManager>().enabled = false;
                GetComponentInParent<EvilNimbusController>().enabled = false;
                weapontrigger1.SetActive(false);
                weapontrigger2.SetActive(false);
                GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, GetComponentInParent<Rigidbody2D>().velocity.y);
                damage_collision.SetActive(false);
                //Access gameover screen
                if (!dead)
                    GetComponentInParent<Animator>().Play("Nimbus_Death");
                dead = true;
                
                GetComponentInParent<Animator>().SetBool("Dead", true);
                //gameObject.SetActive(false);
            }

            if (invincibility_frames > 0)
            {
                invincibility_frames -= Time.deltaTime;

                can_get_hit = false;
            }
            else
            {
                can_get_hit = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            if (can_get_hit)
            {
                if (!other.gameObject.GetComponent<ExplodingBallDamage>())
                    TakeDamage(other.gameObject.GetComponent<DamageEnemy>().damage_to_give);
                else
                    TakeDamage(other.gameObject.GetComponent<ExplodingBallDamage>().damage_to_give);
                FlashRed();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!dead)
        {
            take_damage.Play();
            hit_sound.Play();
            Instantiate(hit_effect, transform.position, Quaternion.identity);

            current_health -= damage;
            invincibility_frames = start_invincibility_frames;

            if (GetComponentInParent<EvilNimbusJump>().is_grounded)
            {
                GetComponentInParent<Animator>().Play("Nimbus_TakeDamage_Ground");
            }
            else
            {
                GetComponentInParent<Animator>().Play("Nimbus_TakeDamage_Air");
            }
        }
    }

    public void FlashRed()
    {
        if (current_health > 0)

            sprite.color = Color.red;
        Invoke("ResetColor", flash_time);
    }
    void ResetColor()
    {
        sprite.color = originalColor;
    }
}
