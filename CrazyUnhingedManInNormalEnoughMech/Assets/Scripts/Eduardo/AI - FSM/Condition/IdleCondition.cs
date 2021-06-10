using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Condition to make the agent reset to the default state
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Condition/IdleCondition")]
    public class IdleCondition : Condition
    {
        public override bool CheckCondition(AgentBehaviour agent)
        {
            return true;
        }
    }
}

