using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Base class for the agents actions
    /// </summary>
    public abstract class AgentAction : ScriptableObject
    {
        public virtual bool checkCondition(AgentBehaviour agent, Condition c)
        {
            if (c.CheckCondition(agent))
                performAction(agent);

            return false;
        }

        public abstract void performAction(AgentBehaviour agent, AgentBehaviour target);

        public virtual void performAction(AgentBehaviour agent, AgentState s) { }

        public virtual void performAction(AgentBehaviour agent) { }

    }

}

