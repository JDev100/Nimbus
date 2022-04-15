using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcessGameScript : MonoBehaviour
{
    bool showPopUp;
    public GameObject howtoplayScreen;
    // Start is called before the first frame update
    void Start()
    {
        showPopUp = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnterMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void EnterCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void ToggleCanvas(string s)
    {
        GameObject.Find(s).SetActive(!GameObject.Find(s).activeSelf);
    }
    public void ToggleHowToPlay()
    {
        howtoplayScreen.SetActive(!howtoplayScreen.activeSelf);
    }
    public void StartMusic()
    {
        FindObjectOfType<MusicController>().musicCanPlay = true;
    }
}