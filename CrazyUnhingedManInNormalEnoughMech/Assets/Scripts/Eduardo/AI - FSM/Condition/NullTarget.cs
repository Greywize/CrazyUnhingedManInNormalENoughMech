using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Check if the the Agents current target is NULL
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Condition/NullTarget")]
    public class NullTarget : Condition
    {
        public override bool CheckCondition(AgentBehaviour agent)
        {
            return (agent.target == null);
        }
    }
}
