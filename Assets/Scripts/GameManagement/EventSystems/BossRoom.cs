using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour {
    // public List<EnemyStatsManager> enemies = new List<EnemyStatsManager>();
    public EnemyStatsManager boss;
    public EvilNimbusHealth dopple;
    public Transform entrance;
    public int boss_music;
    private int original_music = 5;

    public GameObject barriers;
    public Transform spawnPoint;
    
    private bool locked = false;
    private bool has_won = false;

    public bool is_final = false;
    public bool is_dopple = false;
	// Use this for initialization
	void Start () {
       
        barriers.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        if (!is_dopple)
            boss.GetComponent<BOSSInputManager>().standby = true;
        else
            dopple.gameObject.GetComponentInParent<EvilNimbusInputManager>().standby = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (locked)
        {
            has_won = CheckVictory();
            if (has_won)
            {
                barriers.SetActive(false);
                locked = false;
                if (!is_final)
                 FindObjectOfType<MusicController>().SwitchTrack(original_music);
                if (is_final)
                Invoke("RollCredits", 5);
            }

            if (locked && FindObjectOfType<PlayerHealthManager>().player_current_health <= 0 && !has_won)
            {
                Reset();
            }
        }
	}
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.transform.position.x > entrance.transform.position.x && Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            if (!locked)
            {
                // original_music = FindObjectOfType<MusicController>().GetCurrentTrack();
                if (!is_dopple)
                    boss.GetComponent<BOSSInputManager>().standby = false;
                else
                    dopple.gameObject.GetComponentInParent<EvilNimbusInputManager>().standby = false;
                GetComponent<BoxCollider2D>().enabled = false;
                barriers.SetActive(true);
                FindObjectOfType<MusicController>().SwitchTrack(boss_music);
                locked = true;
            }
              
        
           
          
        }
    }

    private bool CheckVictory ()
    {
        bool return_val = true;

        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    if (!enemies[i].dead)
        //    {
        //        return_val = false;
        //        break;
        //    }  
        //}
        if (!is_dopple)
        {
            if (!boss.dead)
            {

                return_val = false;

            }
            else
                boss.GetComponent<BOSSInputManager>().standby = true;
        }
        else
        {
            if (dopple.current_health > 0)
            {
                return_val = false;
            }
            else
            {
                dopple.GetComponentInParent<EvilNimbusInputManager>().standby = true;
            }
        }
      

        return return_val;
    }

    private void Reset()
    {
        Debug.Log("Reset");

        GetComponent<BoxCollider2D>().enabled = true;
        locked = false;
        barriers.SetActive(false);
        FindObjectOfType<MusicController>().SwitchTrack(original_music);
        //FindObjectOfType<MusicController>().ChangeVolume(0);
        if (!is_dopple)
        {
            boss.GetComponent<BOSSInputManager>().standby = true;
            boss.gameObject.transform.position = spawnPoint.transform.position;
            boss.gameObject.GetComponent<EnemyHealthManager>().enemy_current_health = boss.GetComponent<EnemyHealthManager>().enemy_max_health;
        }
        else
        {
            Debug.Log("ResetDopple");
            dopple.current_health = dopple.max_health;
            dopple.gameObject.GetComponentInParent<EvilNimbusInputManager>().standby = true;
          //  dopple.gameObject.GetComponentInParent<Transform>().transform.position = spawnPoint.transform.position;
        }
    }
    private void RollCredits()
    {
        SceneManager.LoadScene(2);
    }
}
