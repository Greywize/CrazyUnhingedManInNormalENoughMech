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
            agent.ResetState(agent, agent.currState);
            agent.resetAction(agent);
            agent.ResetTransition(agent);
            
            agent.currentTransition++;

            if (agent.currentTransition >= agent.Transitions.Length)
                agent.currentTransition = 0;
        }

        public virtual void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            Array.Clear(agent.ActionList, 0, agent.ActionList.Length);
            agent.ActionList = actions;
        }

        protected virtual void drawLineDestination(AgentBehaviour agent)
        {
            if (agent.destination != Vector3.zero)
                Debug.DrawLine(agent.currPosition, agent.destination, Color.blue);
        }
    }
}