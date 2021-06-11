using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/State/IdleState")]
    public class IdleState : AgentState
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void OnStateEnter(AgentBehaviour agent)
        {
            Debug.Log($"{agent} has entered Idle State");
        }

        public override void Tick(AgentBehaviour agent)
        {
            // No actions are loaded into this state
            if (agent.actionIndex < agent.agentActions.Length)
                agent.agentActions[agent.actionIndex].Tick(agent);
            else if (agent.actionIndex >= agent.agentActions.Length)
                OnStateExit(agent);
        }

        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            Array.Clear(agent.agentActions, 0, agent.agentActions.Length);
            agent.agentActions = actions;
        }
    }
    
}
