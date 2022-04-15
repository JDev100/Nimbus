using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {
    //The list of items available for chance selection
    public List<DropLoot> lootTable = new List<DropLoot>();

    public int dropChance;
    
	
    public void CalculateLoot(Transform location)
    {
        int calc_drop_chance = Random.Range(0, 101);

        if (calc_drop_chance > dropChance)
        {
            Debug.Log("No Loot");
            return;
        }

        if (calc_drop_chance <= dropChance)
        {
            int item_weight = 0;

            for (int i = 0; i < lootTable.Count; i++)
            {
                item_weight += lootTable[i].dropRarity;
            }

            int random_value = Random.Range(0, item_weight);

            for (int j = 0; j < lootTable.Count; j++)
            {
                if (random_value <= lootTable[j].dropRarity)
                {
                    Instantiate(lootTable[j].item, location.position, Quaternion.identity);
                    return;
                }
                random_value -= lootTable[j].dropRarity;
            }
        }
    }
    public void CalculateLoot(Transform location, int chance)
    {
        int calc_drop_chance = Random.Range(0, 101);

        if (calc_drop_chance > chance)
        {
            Debug.Log("No Loot");
            return;
        }

        if (calc_drop_chance <= chance)
        {
            int item_weight = 0;

            for (int i = 0; i < lootTable.Count; i++)
            {
                item_weight += lootTable[i].dropRarity;
            }

            int random_value = Random.Range(0, item_weight);

            for (int j = 0; j < lootTable.Count; j++)
            {
                if (random_value <= lootTable[j].dropRarity)
                {
                    Instantiate(lootTable[j].item, location.position, Quaternion.identity);
                    return;
                }
                random_value -= lootTable[j].dropRarity;
            }
        }
    }
}

[System.Serializable]
public class DropLoot {
    public string name;
    public GameObject item;
    public int dropRarity;
}


