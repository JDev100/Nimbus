using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour {
    public string ability;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            other.gameObject.GetComponentInParent<PlayerUpgradeManager>().UnlockAbility(ability);
            FindObjectOfType<GetUpgrade>().DisplayUpgradeScreen(ability);
        }
    }
}
