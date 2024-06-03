using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Death", menuName = "ScriptableNodes/ScriptableConditions/Death")]
public class CheckDeath : ScriptableCondition
{
    public override bool Check(EnemyStateController sc)
    {
        var ec = (EnemyController)sc;
        return ec.HP <= 0;
    }
}
