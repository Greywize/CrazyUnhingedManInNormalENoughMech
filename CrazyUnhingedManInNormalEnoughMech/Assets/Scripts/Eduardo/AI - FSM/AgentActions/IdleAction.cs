using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    [CreateAssetMenu(menuName = "AI/AgentAction/IdleAction")]
    public class IdleAction : AgentAction
    {
        public Condition condition;
        
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent)
        {
            if(condition)
                performAction(agent);
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            IdleAction act = ScriptableObject.Instantiate(this);
            agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            IdleAction act = ScriptableObject.Instantiate(this);
            return act;
        }
    }
}