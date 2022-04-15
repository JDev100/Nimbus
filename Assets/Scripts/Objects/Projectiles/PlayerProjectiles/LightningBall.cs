using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : MonoBehaviour {
    private bool shootRight;
    public float horizontal_velocity;
    public float distance;
    public GameObject lighting_strike;

    private RaycastHit2D hit_info;
    public LayerMask what_is_enemy;

    private Rigidbody2D rb;

    //SFX
    private AudioSource lightning_magic;
    private AudioSource lightning_impact;

    // Start is called before the first frame update
    void Start()
    {
        lightning_magic = FindObjectOfType<SFXManager>().FindSFX("lightning_magic");
        lightning_impact = FindObjectOfType<SFXManager>().FindSFX("lightning_impact");
        lightning_magic.Play();

        rb = GetComponent<Rigidbody2D>();
        shootRight = FindObjectOfType<PlayerController>().is_facing_right;

        if (shootRight)
        {
            rb.velocity = new Vector2(horizontal_velocity, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            rb.velocity = new Vector2(-horizontal_velocity, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyHealth")
        {
            Debug.Log("FoundENemy");
            Instantiate(lighting_strike, transform.position, Quaternion.identity);
            lightning_impact.Play();
            Destroy(gameObject);
        }
    }
}
