using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCondition : ScriptableObject
{
     public abstract bool Check(EnemyStateController sc);
    
}
