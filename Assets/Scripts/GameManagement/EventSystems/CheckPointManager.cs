using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {
    private Transform current_checkpoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCheckPoint(Transform checkpoint)
    {
        current_checkpoint = checkpoint;
    }
    public void GoToCheckPoint()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = current_checkpoint.position;

    }
}
