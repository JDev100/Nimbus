using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour {

    private UnlockableAbility dash_move;
    private UnlockableAbility ruptured_inferno;
    private UnlockableAbility sword;
    private UnlockableAbility arc_thunder;
    private UnlockableAbility arcane_frost;
    private UnlockableAbility plasma_grenade;
    private List<UnlockableAbility> ability_list = new List<UnlockableAbility>();
	// Use this for initialization
	void Start () {
        dash_move = new UnlockableAbility();
        dash_move.name = "DashAbility";
        ruptured_inferno = new UnlockableAbility();
        ruptured_inferno.name = "RupturedInferno";
        sword = new UnlockableAbility();
        sword.name = "Sword";
        arc_thunder = new UnlockableAbility();
        arc_thunder.name = "ArcThunder";
        arcane_frost = new UnlockableAbility();
        arcane_frost.name = "ArcaneFrost";
        plasma_grenade = new UnlockableAbility();
        plasma_grenade.name = "PlasmaGrenade";

        ability_list.Add(plasma_grenade);
        ability_list.Add(arcane_frost);
        ability_list.Add(arc_thunder);
        ability_list.Add(sword);
        ability_list.Add(dash_move);
        ability_list.Add(ruptured_inferno);
	}
	
	// Update is called once per frame
	void Update () {
        if (dash_move.unlocked)
        {
            GetComponent<Dash>().unlocked = true;
        }
        if (ruptured_inferno.unlocked)
        {
            GetComponent<NimbusFowardMagic>().unlocked = true;
        }
        if (sword.unlocked)
        {
            GetComponent<NimbusSlash>().unlocked = true;
        }
        if (arc_thunder.unlocked)
        {
            GetComponent<ArcFire>().unlocked = true;
        }
        if (arcane_frost.unlocked)
        {
            GetComponent<HolyFire>().unlocked = true;
        }
        if (plasma_grenade.unlocked)
        {
            GetComponent<ExplodingFire>().unlocked = true;
        }
	}


    public void UnlockAbility(string name)
    {
        FindAbilty(name).unlocked = true;
    }

    public UnlockableAbility FindAbilty (string name)
    {
        UnlockableAbility returnVal = null;

        for (int i = 0; i < ability_list.Count; i++)
        {
            if (ability_list[i].name == name)
            {
                returnVal = ability_list[i];
                break;
            }
        }

        return returnVal;
    }
}



public class UnlockableAbility
{
    public string name;
    public bool unlocked = false;
}