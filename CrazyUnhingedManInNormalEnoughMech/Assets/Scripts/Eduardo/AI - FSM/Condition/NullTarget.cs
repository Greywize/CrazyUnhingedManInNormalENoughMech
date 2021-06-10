using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/Condition/NullTarget")]
    public class NullTarget : Condition
    {
        public override bool CheckCondition(AgentBehaviour agent)
        {
            agent.enableSensor(true);

            return (agent.target == null);
        }
    }

}
