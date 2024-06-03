using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Armors")]
public class EquipmentSO : ItemSO
{
    public int armor;

    private void OnEnable()
    {
        if (armor == 0)
        {
            armor = Random.Range(Rarity*3 + 1, Rarity*3+3);
        }
    }
}
