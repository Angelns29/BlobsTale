using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
    protected EnemyStateController sc;
    public abstract void OnFinishedState();

    public virtual void OnSetState(EnemyStateController sc) {
        this.sc = sc;
    }

    public abstract void OnUpdate();
}
