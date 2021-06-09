using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/SeekTarget")]
    public class SeekTarget : AgentState
    {
        public AgentAction[] actions;

        private void OnDrawGizmosSelected()
        {
            // Draw patrol points when agent is selected
            // for(int i = 0; i < defaultState)
        }

        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.EnableSensor(true);

            if (agent.target == null)
            {
                Debug.Log($"{agent} is in state: {this} and has no target!");
                OnStateExit(agent);
                return;
            }

            addActions(agent, actions);
            agent.actionCondition = agent.agentActions[agent.actionIndex].getCondition();
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (agent.target == null)
            {
                Debug.Log($"{agent} has no target!?");
                OnStateExit(agent);
                return;
            }
            else
            {
                agent.destination = agent.target.transform.position;
            }

            // Perform Actions
            if (agent.actionIndex < agent.agentActions.Length)
                agent.agentActions[agent.actionIndex].Tick(agent);
            else if (agent.actionIndex >= agent.agentActions.Length)
                OnStateExit(agent);
        }

        /// <summary>
        /// Add the action(s) to the agent
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="action"></param>
        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            Array.Clear(agent.agentActions, 0, agent.agentActions.Length);

            for (int i = 0; i < actions.Length; i++)
            {
                agent.agentActions[i] = actions[i].addinstance(agent);
            }
        }
    }
}