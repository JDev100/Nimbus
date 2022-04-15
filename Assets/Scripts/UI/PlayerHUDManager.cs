using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour {
    public PlayerBar health_bar;
    public PlayerBar magic_bar;
    public Image spell_display;
    public PlayerBar currency_display;

    public List<SpellDisplay> spells = new List<SpellDisplay>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public SpellDisplay FindSpell(string name)
    {
        SpellDisplay returnVal = null;

        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].name == name)
            {
                returnVal = spells[i];
                break;
            }
        }

        return returnVal;
    }
}
[System.Serializable]
public class PlayerBar
{
    public Image bar;
    public Image follow_bar;
    public Text bar_text = null;
}

[System.Serializable]
public class SpellDisplay
{
    public string name;
    public Sprite sprite;
}
