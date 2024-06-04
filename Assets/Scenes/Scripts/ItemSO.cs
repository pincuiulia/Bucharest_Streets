using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = StatToChange.none;
    public int amountToChangeStat;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            HealthBar healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (healthBar.slider.value == healthBar.slider.maxValue)
            {
                return false;
            }
            else
            {
                healthBar.IncreaseHealth(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    }
}
