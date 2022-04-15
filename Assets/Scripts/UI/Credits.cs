using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("CreditsIdle"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(0);
            }
        }

    }

}
