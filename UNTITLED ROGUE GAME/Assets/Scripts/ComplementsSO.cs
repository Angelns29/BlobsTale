using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Complements")]
public class ComplementsSO : ItemSO
{

    public float statMultiplier;

    public string[] stats = { "health", "armor" };

    public string multiplier;

    private void Start()
    {
        multiplier = stats[Random.Range(0, stats.Length)];

        statMultiplier = 1 + (Random.Range(10*Rarity+10, 10 * Rarity +20)/100);
    }

}
