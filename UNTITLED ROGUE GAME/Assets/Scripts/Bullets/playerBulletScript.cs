using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Stats;
using UnityEngine;

public class playerBulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) gameObject.SetActive(false);
        var temp = collision.gameObject.GetComponent<EnemyController>() as IDamageable;
        if (temp != null)
        {
            temp.OnHurt(5);

            gameObject.SetActive(false);
        }
    }
}
