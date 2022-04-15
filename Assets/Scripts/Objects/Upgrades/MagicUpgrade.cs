using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUpgrade : MonoBehaviour {
    public int magicToUpgrade;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHealth")
        {
            other.gameObject.GetComponentInParent<PlayerMagicManager>().UpgradeMagic(magicToUpgrade);
            Destroy(gameObject);
            FindObjectOfType<GetUpgrade>().GetComponent<GetUpgrade>().DisplayUpgradeScreen("MagicUpgrade");
        }
    }
}
