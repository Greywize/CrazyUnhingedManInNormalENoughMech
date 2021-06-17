using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    /// <summary>
    /// Detect all agents in proximity (sensorProximity)
    /// </summary>
    [CreateAssetMenu(menuName = "AI/AgentAction/DectectAgentAction")]
    public class DetectAgentAction : AgentAction
    {

        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            throw new System.NotImplementedException();
        }

        public override void performAction(AgentBehaviour agent)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            throw new System.NotImplementedException();
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            DetectAgentAction act = ScriptableObject.Instantiate(this);
            return act;
        }
    }
}
