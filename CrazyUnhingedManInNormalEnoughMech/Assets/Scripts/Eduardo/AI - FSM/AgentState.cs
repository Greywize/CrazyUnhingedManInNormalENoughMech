using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class AgentState : ScriptableObject
    {
        [SerializeField] protected AgentAction[] _actions;

        public virtual AgentAction[] GETActions()
        {
            return _actions;
        }
        
        public abstract void OnStateEnter(AgentBehaviour agent);

        public abstract void Tick(AgentBehaviour agent);

        public virtual void Tick(AgentBehaviour agent, PatrolState p) 
        {
            Debug.Log($"{agent} has no definition for PatrolState Tick {p}");
        }
        public virtual void Tick(AgentBehaviour agent, SeekDestination sd) 
        {
            Debug.Log($"{agent} has no definition for SeekDestination tick {sd}");
        }

        public virtual void OnStateExit(AgentBehaviour agent, AgentState s) 
        {
            Debug.Log($"{agent} has no definition for OnStateExit {s}");
        }

        public virtual void OnStateExit(AgentBehaviour agent, PatrolState p) 
        {
            Debug.Log($"{agent} has no definition for PatrolState OnStateExit {p}");
        }

        public virtual void OnStateExit(AgentBehaviour agent)
        {
            agent.ResetAgent(agent, agent.currState);
            agent.ResetTransition(agent);
            agent.resetAction(agent);
        }

        public abstract void addActions(AgentBehaviour agent, AgentAction[] actions);
    }

}
