using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Condition/CurrentStateNull")]
public class NullState : Condition
{
    public override bool CheckCondition(AgentBehaviour agent)
    {
        return (agent.currState == null);
    }
}
