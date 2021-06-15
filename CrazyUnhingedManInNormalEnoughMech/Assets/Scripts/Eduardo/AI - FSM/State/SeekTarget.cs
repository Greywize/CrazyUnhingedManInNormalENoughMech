using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/State/SeekTarget")]
    public class SeekTarget : AgentState
    {
        public override void OnStateEnter(AgentBehaviour agent)
        {
            //   agent.EnableSensor(true);        // Create a detectAgentAction
            addActions(agent, _actions);
            agent.actionCondition = agent.ActionList[agent.actionIndex].getCondition();
        }

        public override void Tick(AgentBehaviour agent)
        {
            setTarget(agent);
            drawLineDestination(agent);

            // Perform Actions
            if (agent.actionIndex < agent.ActionList.Length)
            {
                drawLineDestination(agent);
                agent.ActionList[agent.actionIndex].Tick(agent);
            }
                

            if (agent.actionIndex >= agent.ActionList.Length)
                OnStateExit(agent);
        }

        private void setTarget(AgentBehaviour agent)
        {
            if (agent.target == null)
            {
                Debug.Log($"{agent} has no target!?");
                OnStateExit(agent);
                return;
            }
            else
                agent.destination = agent.target.transform.position;
        }
    }
}