using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyManager : MonoBehaviour {

    public int max_currency;
    public static int current_currency;

    private Text currency_text;
	// Use this for initialization
	void Start () {
        current_currency = 0;
        currency_text = FindObjectOfType<PlayerHUDManager>().currency_display.bar_text;
	}
	
	// Update is called once per frame
	void Update () {
        //Display current currency in UI
        currency_text.text = "" + current_currency;

        //Make sure player doesn't go over max currency
        if (current_currency > max_currency)
        {
            current_currency = max_currency;
        }
	}

    public void AddCurrency(int money)
    {
        current_currency += money;
    }
    public void SpendCurrency(int money)
    {
        current_currency -= money;
    }
}
