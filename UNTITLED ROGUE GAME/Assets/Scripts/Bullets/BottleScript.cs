using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Stats;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.SetTrigger("Splash");
        }
        var temp = collision.gameObject.GetComponent<CharacterStatsHandler>() as IDamageable;
        if (temp != null)
        {
            rb.gravityScale = 0;
            temp.OnHurt(10);
            animator.SetTrigger("Splash");
        }
    }
}
