using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float start_self_destruct_time;
    private float self_destruct_time;

	// Use this for initialization
	void Start () {
        self_destruct_time = start_self_destruct_time;

        StartCoroutine(SelfDestructCounter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SelfDestructCounter()
    {
        yield return new WaitForSeconds(self_destruct_time);

        Destroy(gameObject);
    }
}
