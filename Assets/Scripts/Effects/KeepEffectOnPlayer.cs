using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepEffectOnPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = FindObjectOfType<PlayerController>().transform.position;
	}
}
