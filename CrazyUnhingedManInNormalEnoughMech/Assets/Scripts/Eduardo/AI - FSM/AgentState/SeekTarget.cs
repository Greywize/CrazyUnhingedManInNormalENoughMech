using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/SeekTarget")]
    public class SeekTarget : AgentState
    {
<<<<<<< HEAD
=======
        public AgentAction[] actions;
        
        
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.EnableSensor(true);

            if (agent.target == null)
            {
                Debug.Log($"{agent} is in state: {this} and has no target!");
                OnStateExit(agent);
                return;
            }
<<<<<<< HEAD

            addActions(agent, actions);
            agent.actionCondition = agent.agentActions[agent.actionIndex].getCondition();
=======
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (agent.target == null)
            {
                OnStateExit(agent);
            }

            agent.destination = agent.target.transform.position;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);
            Debug.DrawLine(agent.target.transform.position, agent.transform.position, Color.red);
<<<<<<< HEAD

            if (distance > agent.sensor.detectRadius)
                agent.MoveToward(agent.destination);
            else
            {
                agent.enableSensor(true);
                OnStateExit(agent);
=======

<<<<<<< HEAD
            // Perform Actions
            if (agent.actionIndex < agent.agentActions.Length)
                agent.agentActions[agent.actionIndex].Tick(agent);
            else if (agent.actionIndex >= agent.agentActions.Length)
=======
            if (distance > agent.sensor.detectRadius)
                agent.MoveToward(agent.destination);
            else
            {
                addActions(agent, actions);
                agent.enableSensor(true);
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
                OnStateExit(agent);
            }
        }

        /// <summary>
        /// Add the action to the agent
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="action"></param>
        private void addActions(AgentBehaviour agent, AgentAction[] action)
        {
            for (int i = 0; i < actions.Length; i++)
            {
               // actions[i] = ScriptableObject.CreateInstance(actions[i]);    
                agent.agentActions[i] = actions[i];
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
            }
                
        }
    }
}
