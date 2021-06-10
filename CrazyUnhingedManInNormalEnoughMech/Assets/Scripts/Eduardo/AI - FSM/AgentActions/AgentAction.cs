using System;
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
        public abstract void performAction(AgentBehaviour agent, AgentBehaviour target);

        public virtual void performAction(AgentBehaviour agent, AgentState s)
        {
        }

        public virtual void performAction(AgentBehaviour agent)
        {
            // Debug.Log($"{agent} is performing {this}");
        }

        public abstract void Tick(AgentBehaviour agent, Condition cond);

        public abstract void Tick(AgentBehaviour agent);

        public virtual void onExit(AgentBehaviour agent)
        {
            agent.agentActions[agent.currAction] = null;
            agent.currAction++;
        }

        public abstract void addAction(AgentBehaviour agent, int index);

        public abstract AgentAction addinstance(AgentBehaviour agent);
    }
}