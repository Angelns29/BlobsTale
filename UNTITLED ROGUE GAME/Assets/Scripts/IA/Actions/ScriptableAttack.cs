using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableAttack", menuName =
    "ScriptableObjects2/ScriptableAction/ScriptableAttack", order = 1)]
public class ScriptableAttack : ScriptableAction
{
    public Animator animator;
    public EnemyController enemyController;
    private float horizontal;

    public override void OnFinishedState()
    {
        //GameManager.gm.UpdateText("va te perdono");
        animator.Play("Movement");
    }

    public override void OnSetState(EnemyStateController sc)
    {
        base.OnSetState(sc);
        animator = sc.GetComponent<Animator>();
        enemyController = sc.GetComponent<EnemyController>();
        horizontal = animator.GetFloat("Horizontal");
        animator.Play("Attack");
        if (enemyController.bulletPrefab != null) enemyController.LaunchBullet();
        else if (enemyController.sword != null)
        {
            var sword = enemyController.sword.GetComponent<RotateSword>();
            sword.RotateArm(horizontal);
            sword.Attack();
        }
        //GameManager.gm.UpdateText("a q te meto");
    }
    
    public override void OnUpdate()
    {
       // GameManager.gm.UpdateText("que te meto asin");
    }
}
