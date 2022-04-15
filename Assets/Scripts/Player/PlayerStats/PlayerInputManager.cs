using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchInput(bool val)
    {
        GetComponent<PlayerController>().can_input = val;
        GetComponent<PlayerJump>().can_input = val;
        GetComponent<NimbusSlash>().can_input = val;
        GetComponent<NimbusFowardMagic>().can_input = val;
        GetComponent<ArcFire>().can_input = val;
        //GetComponent<HolyFire>().ca
    } 

    
}
