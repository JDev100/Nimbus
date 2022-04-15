using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpikes : MonoBehaviour {
    public float rotation_speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * rotation_speed * Time.deltaTime);
	}
}
