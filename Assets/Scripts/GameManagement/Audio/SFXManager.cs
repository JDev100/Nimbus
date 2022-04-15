using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    public List<SFXList> sfxLists = new List<SFXList>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public AudioSource FindSFX(string name)
    {
        AudioSource returnVal = null;
        for (int i = 0; i < sfxLists.Count; i++)
        {
            if (name == sfxLists[i].name)
            {
                returnVal =  sfxLists[i].sfx;
                break;
            }
        }
        return returnVal;
    }
}
[System.Serializable]
public class SFXList {
    public string name;
    public AudioSource sfx;
}

