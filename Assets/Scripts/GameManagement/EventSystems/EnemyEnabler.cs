using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabler : MonoBehaviour
{
    public bool activePrimed;
    public GameObject[] enemy;
    public float respawnTimer;
    public float respawnCountdown;

    Renderer theRenderer;

    // Start is called before the first frame update
    void Start()
    {
        theRenderer = GetComponent<Renderer>();
        GetComponent<EnemyHealthManager>();
        GetComponent<EnemyStatsManager>();
        respawnCountdown = respawnTimer;
        activePrimed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (theRenderer.isVisible)
        {
            for (int i = 0; i < enemy.Length - 1; i++)
            {
                if (enemy[i].GetComponent<EnemyStatsManager>().dead == true)
                {
                    enemy[i].GetComponent<EnemyStatsManager>().activePrimed = true;
                    activePrimed = true;
                }
            }
            
        }

        if (!theRenderer.isVisible && activePrimed)
        {
            respawnCountdown -= Time.deltaTime;
            if (respawnCountdown <= 0)
            {
                respawnCountdown = 0;
            }
        }

        if (!theRenderer.isVisible && activePrimed && respawnCountdown <= 0)
        {
            //Debug.Log("Enemy Respawned");

            for (int i = 0; i < enemy.Length - 1; i++)
            {
                if (enemy[i].GetComponent<EnemyStatsManager>().activePrimed)
                {
                    enemy[i].GetComponent<EnemyHealthManager>().enemy_current_health = enemy[i].GetComponent<EnemyHealthManager>().enemy_max_health;
                    enemy[i].GetComponent<EnemyStatsManager>().dead = false;
                    enemy[i].SetActive(true);
                   
                }
            }
            respawnCountdown = respawnTimer;
            activePrimed = false;
        }
    }
    
    //void OnBecameInvisible()
    //{
    //    if (activePrimed == true && respawnCountdown <= 0)
    //    {
    //        Debug.Log("Enemy Respawned");

    //        enemy.GetComponent<EnemyHealthManager>().enemy_current_health = enemy.GetComponent<EnemyHealthManager>().enemy_max_health;
    //        enemy.GetComponent<EnemyStatsManager>().dead = false;
    //        enemy.SetActive(true);
    //        respawnCountdown = respawnTimer;
    //        activePrimed = false;

    //    }
    //}
}
