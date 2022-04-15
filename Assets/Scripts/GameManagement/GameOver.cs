using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    bool game_has_ended;
    public GameObject gameOverUI;
    public bool has_reset = false;

    //respawn variables
    private GameObject nimbus;
    private PlayerHealthManager playerHealth;
    private PlayerMagicManager playerMagic;
    private NimbusSpawner nimbusSpawn;
    public float time_to_restart;
    // Use this for initialization
    void Start () {
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerMagic = FindObjectOfType<PlayerMagicManager>();
        nimbusSpawn = FindObjectOfType<NimbusSpawner>();
        nimbus = GameObject.FindGameObjectWithTag("Player");
    }

    public void EndGame()
    {
        if (game_has_ended == false)
        {
            game_has_ended = true;
            Debug.Log("GAME OVER");

            //Bring up Gameover menu
            gameOverUI.SetActive(true);
            //FindObjectOfType<MusicController>().ChangeVolume(0);
        }
    }

    public void PressRestart()
    {
        Invoke("Restart", time_to_restart);
    }

    public void Restart()
    {
        //Respawns player at latest checkpoint
        game_has_ended = false;
        gameOverUI.SetActive(false);
        nimbus.transform.position = nimbus.GetComponent<NimbusSpawner>().spawnPoint.transform.position;
        nimbus.GetComponent<Animator>().SetBool("Dead", false);
        playerHealth.dead = false;
        playerHealth.health_collision.SetActive(true);
        playerHealth.player_current_health = playerHealth.player_max_health;
        playerMagic.currentMagic = playerMagic.maxMagic;
        nimbus.GetComponent<PlayerInputManager>().SwitchInput(true);
        playerHealth.SetDeath(false);
        nimbus.GetComponent<Animator>().Play("Nimbus_Idle");
        FindObjectOfType<MusicController>().ChangeVolume(1);
        has_reset = true;
    }
}
