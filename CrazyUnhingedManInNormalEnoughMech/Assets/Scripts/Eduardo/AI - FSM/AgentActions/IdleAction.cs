using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    [CreateAssetMenu(menuName = "AI/AgentAction/IdleAction")]
    public class IdleAction : AgentAction
    {

        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            Debug.Log($"{agent} has reached its destination");
        }
    }
}