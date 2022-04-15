using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour {
    private MusicController mc;

    public int newTrack;

    public bool switch_on_start;
    public bool do_once = true;
    
	// Use this for initialization
	void Start () {
        mc = FindObjectOfType<MusicController>();

        if (switch_on_start)
        {
            mc.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchTrack()
    {

        mc.SwitchTrack(newTrack);
    }

    //Switch music when entering area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            mc.SwitchTrack(newTrack);
            if(do_once)
            gameObject.SetActive(false);
        }
    }
}
