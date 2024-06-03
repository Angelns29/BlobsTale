using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Stats;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var temp = collision.gameObject.GetComponent<CharacterStatsHandler>() as IDamageable;
        if (temp != null)
        {
            temp.OnHurt(15);
        }
    }
}
