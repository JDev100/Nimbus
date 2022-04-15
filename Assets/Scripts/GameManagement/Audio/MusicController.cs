using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public AudioSource[] musicTracks;
    public int currentTrack;
    public bool musicCanPlay = true;

    private float max_volume = 1f;
    public float current_volume;
	// Use this for initialization
	void Start () {
        current_volume = max_volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (musicCanPlay)
        {
            if (!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].Play();
                max_volume = musicTracks[currentTrack].volume;
            }
        }
        else
        {
            musicTracks[currentTrack].Stop();
        }
	}
    public int GetCurrentTrack()
    {
        return currentTrack;
    }

    //Used for switching track
    public void SwitchTrack (int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }

    public void NextTrack ()
    {
        musicTracks[currentTrack].Stop();
        if (currentTrack != musicTracks.Length - 1)
            currentTrack++;
        else
            currentTrack = 0;
        musicTracks[currentTrack].Play();
    }

    public void PreviousTrack()
    {
        musicTracks[currentTrack].Stop();
        if (currentTrack != 0)
            currentTrack--;
        else
            currentTrack = musicTracks.Length - 1;
        musicTracks[currentTrack].Play();
    }

    public void ChangeVolume (float volume)
    {
        musicTracks[currentTrack].volume = volume;
    }
    public void ResetVolume ()
    {
        musicTracks[currentTrack].volume = max_volume;
    }
}
