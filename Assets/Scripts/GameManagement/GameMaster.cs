using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    public static bool exists;

    public Transform spawn_point;
    // Use this for initialization
    void Start()
    {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "ForestTest")
        {
            FindObjectOfType<PlayerController>().transform.position = spawn_point.position;
        }
    }
	// Update is called once per frame
	void Update () {
		if (FindObjectsOfType<PlayerController>() != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;

            if (sceneName == "MainMenu")
            {
                Debug.Log("In Main Menu");
                FindObjectOfType<PlayerController>().gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Good to go");
                FindObjectOfType<PlayerController>().gameObject.SetActive(true);
            }
        }
	}
}
