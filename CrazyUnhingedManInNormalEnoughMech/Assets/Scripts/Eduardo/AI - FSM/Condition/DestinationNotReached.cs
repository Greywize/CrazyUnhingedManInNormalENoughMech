using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/Condition/DestinationNotReached")]
    public class DestinationNotReached : Condition
    {
        public override bool CheckCondition(AgentBehaviour agent)
        {
            float distFromDestination = Vector3.Distance(agent.currPosition, agent.destination);
            return (distFromDestination >= agent.actionRange ? true : false);
        }
    }
}
