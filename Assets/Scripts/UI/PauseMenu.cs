using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUi;

    //SFX
    private AudioSource open_pause_menu;
    private AudioSource close_pause_menu;
    public float low_volume;
    // Update is called once per frame
    private void Start()
    {
        //Set up sfx
        open_pause_menu = FindObjectOfType<SFXManager>().FindSFX("open_pause_menu");
        close_pause_menu = FindObjectOfType<SFXManager>().FindSFX("close_pause_menu");
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        close_pause_menu.Play();
        FindObjectOfType<MusicController>().ResetVolume();
    }

    void Pause()
    {
        open_pause_menu.Play();
        FindObjectOfType<MusicController>().ChangeVolume(low_volume);

        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //public void ResumeGame()
    //{
    //    pauseMenuUi.SetActive(false);
    //    Time.timeScale = 1f;
    //    GameIsPaused = false;
    //}

    //For switching tracks
    public void NextTrack()
    {
        FindObjectOfType<MusicController>().NextTrack();
    }
    public void PreviousTrack()
    {
        FindObjectOfType<MusicController>().PreviousTrack();
    }

}
