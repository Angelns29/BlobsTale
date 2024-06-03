using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
[CreateAssetMenu(fileName = "ScriptableFollow", menuName = "ScriptableObjects2/ScriptableAction/ScriptableFollow")]

public class ScriptableFollow : ScriptableAction
{
    private EnemyMovement _chaseBehaviour;
    private EnemyController _enemyController;
    public override void OnFinishedState()
    {
        _chaseBehaviour.StopChasing();
    }

    public override void OnSetState(EnemyStateController sc)
    {
        base.OnSetState(sc);
        //GameManager.gm.UpdateText("Te persigo");
        _chaseBehaviour = sc.GetComponent<EnemyMovement>();
        _enemyController = (EnemyController)sc;
    }

    public override void OnUpdate()
    {
        _chaseBehaviour.Chase(_enemyController.target.transform, sc.transform);
        //_enemyController.RotateSword();
    }
}
