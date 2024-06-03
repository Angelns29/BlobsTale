using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Target", menuName = "ScriptableNodes/ScriptableConditions/Root")]
public class CheckRoot : ScriptableCondition
{
    public override bool Check(EnemyStateController sc)
    {
        return true;
    }
}
