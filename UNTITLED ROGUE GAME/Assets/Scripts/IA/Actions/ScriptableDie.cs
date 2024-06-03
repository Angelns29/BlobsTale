using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableDie", menuName = "ScriptableObjects2/ScriptableAction/ScriptableDie", order = 2)]

public class ScriptableDie : ScriptableAction
{
    public override void OnFinishedState()
    {
        //GameManager.gm.UpdateText("me mori");
    }

    public override void OnSetState(EnemyStateController sc)
    {
        base.OnSetState(sc);
       // GameManager.gm.UpdateText("me estoy muriendo");
    }

    public override void OnUpdate()
    {
        //GameManager.gm.UpdateText("toma mis monedas");
    }
}
