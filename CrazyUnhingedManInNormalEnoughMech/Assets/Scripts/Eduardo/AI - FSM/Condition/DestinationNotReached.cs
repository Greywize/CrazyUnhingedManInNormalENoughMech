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
            return (Vector3.Distance(agent.currPosition, agent.destination) > agent.sensor.detectRadius ? true : false);
        }
    }
}
