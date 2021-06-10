using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/Condition/NullDestination")]
    public class NullDestination : Condition
    {
        public override bool CheckCondition(AgentBehaviour agent)
        {
            return (agent.destination == Vector3.zero);
        }
    }
}

