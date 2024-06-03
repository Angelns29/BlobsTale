using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public NodeBehaviourTree currentState;
    public GameObject target = null;
    //protected NodeBehaviourTree stateToPlay = null;
    public void StateTransition()
    {
        if (!currentState.AbortCondition(this))
        {
            if (currentState.Children.Count != 0)
            {
                bool cond = false;
                int count = 0;
                while (!cond && count != currentState.Children.Count)
                {
                    cond = CheckCondition(currentState.Children[count++]);
                }
                if (cond)
                {
                    if (currentState.action != null) currentState.action.OnFinishedState();
                    currentState = currentState.Children[count - 1];
                    if (currentState.action != null) currentState.action.OnSetState(this);
                }
            }
        }
        else
            GoToRootState();
    }
    public void GoToRootState()
    {
        if (currentState.Parent != null)
        {
            if (currentState.action != null) currentState.action.OnFinishedState();
            currentState = currentState.Parent;
            GoToRootState();
        }
    }
    public bool CheckCondition(NodeBehaviourTree node)
    {
        return node.Condition(this);
    }
}
