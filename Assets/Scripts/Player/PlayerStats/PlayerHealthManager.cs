using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int player_max_health;
    public float player_current_health;
    public float start_invincibility_frames;
    private float invincibility_frames;

    public GameObject health_collision;
    //For displaying healeffect
    public GameObject heal_effect;
    public bool dead = false;

    public float deplete_speed;

    //For flashing player when hit
    private SpriteRenderer sprite;

    //SFX
    private AudioSource take_damage;
    
    void Start () {
        //Gets access to player sprite
        sprite = GetComponent<SpriteRenderer>();
        take_damage = FindObjectOfType<SFXManager>().FindSFX("take_damage");

        player_current_health = player_max_health;

        // Initializes the player's health to given health values
    }
	
	// Update is called once per frame
	void Update () {

        //changes the health bar and text based on player's current health
        if (FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount > player_current_health / player_max_health)
        {
            FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount = player_current_health / player_max_health;
        }
        if (FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount < player_current_health / player_max_health)
        {
            FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount = player_current_health / player_max_health;
        }
        if (FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount < FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount)
        {
            
            FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount += deplete_speed * Time.deltaTime;
        }
        
        if (FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount >
            FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount = FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount;
        }

        if (FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount >
            FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount -= deplete_speed * Time.deltaTime;
        }
        if (FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount <
            FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount)
        {
            FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.fillAmount = FindObjectOfType<PlayerHUDManager>().health_bar.bar.fillAmount;
        }
        

       

            //Player dies if health reaches 0
            if (player_current_health <= 0)
        {
            //Access gameover screen
            FindObjectOfType<GameOver>().EndGame();
            GetComponent<PlayerInputManager>().SwitchInput(false);
            if (!dead)
               GetComponent<Animator>().Play("Nimbus_Death");
            dead = true;

            SetDeath(true);

            health_collision.SetActive(false);
            GetComponent<Animator>().SetBool("Dead", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            //gameObject.SetActive(false);

            
        }

        //Temporary Invincibilty
        if (invincibility_frames > 0)
        {
            invincibility_frames -= Time.deltaTime;
            health_collision.gameObject.SetActive(false);

            //Flash player when hit
            if (invincibility_frames < start_invincibility_frames)
            {
                sprite.color = new Color(1, 1, 1, 0.5f);
            }
            if (invincibility_frames < start_invincibility_frames * 0.7f)
            {
                sprite.color = new Color(1, 1, 1, 1f);
            }
            else if (invincibility_frames < start_invincibility_frames * 0.3f)
            {
                sprite.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                sprite.color = new Color(1, 1, 1, 1f);
            }
        }
        else
        {
            health_collision.gameObject.SetActive(true);
        }

        //Make sure player doesnt go over max health
        if (player_current_health > player_max_health)
        {
            player_current_health = player_max_health;

            
        }
        
    }

    //Take Damage
    public void TakeDamage(int damage)
    {
        if (!dead)
        {
            take_damage.Play();

            player_current_health -= damage;
            invincibility_frames = start_invincibility_frames;

            if (GetComponent<PlayerJump>().is_grounded)
            {
                GetComponent<Animator>().Play("Nimbus_TakeDamage_Ground");
            }
            else
            {
                GetComponent<Animator>().Play("Nimbus_TakeDamage_Air");
            }
        }
        FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.color = Color.white;
    }

    //Get health
    public void RaiseHealth(int health_to_raise)
    {
        player_current_health += health_to_raise;

        //Display heal effect
        Instantiate(heal_effect, transform.position, Quaternion.identity);

        FindObjectOfType<PlayerHUDManager>().health_bar.follow_bar.color = Color.green;
    }

    //Upgrade Health
    public void UpgradeHealth(int health_to_upgrade)
    {
        player_max_health += health_to_upgrade;
        player_current_health = player_max_health;
    }

    //For ghosting through enemies when hurt
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (invincibility_frames > 0f)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            }
        }
    }

    public void SetDeath(bool val)
    {
        if (val == true)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerJump>().enabled = false;
            GetComponent<NimbusSlash>().enabled = false;
            GetComponent<SpellSwitcher>().enabled = false;
            GetComponent<NimbusFowardMagic>().enabled = false;
            GetComponent<ArcFire>().enabled = false;
            GetComponent<ExplodingFire>().enabled = false;
            GetComponent<HolyFire>().enabled = false;
            GetComponent<WallJump>().enabled = false;
        }
        else
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerJump>().enabled = true;
            GetComponent<NimbusSlash>().enabled = true;
            GetComponent<SpellSwitcher>().enabled = true;
            GetComponent<NimbusFowardMagic>().enabled = true;
            GetComponent<ArcFire>().enabled = true;
            GetComponent<ExplodingFire>().enabled = true;
            GetComponent<HolyFire>().enabled = true;
            GetComponent<WallJump>().enabled = true;
        }
    }
}
