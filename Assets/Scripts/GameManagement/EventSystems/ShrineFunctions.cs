using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineFunctions : MonoBehaviour
{
    BoxCollider2D activeArea;
    private PlayerHealthManager playerHealth;
    private PlayerMagicManager playerMagic;
    public GameObject spawnPoint;
    private NimbusSpawner playerSpawn;
    public Renderer renderer;

    bool has_saved;
    bool save_once;
    private Animator anim;

    private AudioSource save; 

    void Start()
    {
        save = FindObjectOfType<SFXManager>().FindSFX("save");

        anim = GetComponent<Animator>();
        activeArea = GetComponent<BoxCollider2D>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerMagic = FindObjectOfType<PlayerMagicManager>();
        playerSpawn = FindObjectOfType<NimbusSpawner>();
    }

    void Update()
    {
        has_saved = false;

        if (!renderer.isVisible)
        {
            has_saved = true;
            save_once = false;
        }
        
        anim.SetBool("Good", has_saved);
    }

    void OnTriggerStay2D(Collider2D activeArea)
    {
        if (Input.GetAxisRaw("Vertical") < -0.5)
        {
            Debug.Log("Shrine Activated");
            if (!save_once)
            save.Play();
            playerHealth.player_current_health = playerHealth.player_max_health;
            playerMagic.currentMagic = playerMagic.maxMagic;
            playerSpawn.spawnPoint.position = spawnPoint.transform.position;
            Debug.Log("Checkpoint!");
            anim.SetTrigger("Save");
            save_once = true;
        }
    }

}
