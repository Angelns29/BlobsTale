using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    public void Chase(Transform target, Transform self)
    {
        //_rb.velocity = (target.position - self.position).normalized * Speed;
        Vector2 direction = (target.position - self.position).normalized ;
        _rb.velocity = direction * Speed;
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);

    }
    public void Run(Transform target, Transform self)
    {
        _rb.velocity = (target.position - self.position).normalized * -Speed;
    }

    public void StopChasing()
    {
        _rb.velocity = Vector2.zero;
    }
}
