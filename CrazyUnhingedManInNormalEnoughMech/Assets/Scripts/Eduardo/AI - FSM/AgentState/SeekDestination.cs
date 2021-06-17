using System;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/SeekDestination")]
    public class SeekDestination : AgentState
    {
        [SerializeField] public Vector3 destination;

        /// <summary>
        /// When this state becomes the instantiated behaviour
        /// </summary>
        /// <param name="agent"></param>
        public override void OnStateEnter(AgentBehaviour agent)
        {
            if (agent.currState == null)
                agent.currState = this;

            agent.destination = destination;
<<<<<<< HEAD
            addActions(agent, actions);
=======
            agent.lookAtTarget();
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        }

        /// <summary>
        /// Single purpose state to only seek one destination
        /// </summary>
        /// <param name="agent"></param>
        public override void Tick(AgentBehaviour agent)
        {
<<<<<<< HEAD
            agent.destination = destination;
            
            if (agent.actionIndex < agent.agentActions.Length)
                agent.agentActions[agent.actionIndex].Tick(agent);
            else if(agent.actionIndex >= agent.agentActions.Length)
=======
            Debug.DrawLine(agent.transform.position, agent.destination, Color.green);
            agent.destination = destination;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);

            if (distance > 2.0f)
                agent.MoveToward(destination);
            else
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
                OnStateExit(agent);
        }

        /// <summary>
        /// Update used when this object is used in a Patrol State
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="ps"></param>
        public override void Tick(AgentBehaviour agent, PatrolState ps)
        {
            Debug.DrawLine(agent.transform.position, agent.destination, Color.blue);
            agent.destination = destination;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);

            if (distance > 2.0f)
                agent.MoveToward(destination);
            else
                ps.nextDestination(agent, ps);
        }

<<<<<<< HEAD
        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            Array.Clear(agent.agentActions, 0, agent.agentActions.Length);

            for (int i = 0; i < actions.Length; i++)
            {
                agent.agentActions[i] = actions[i].addinstance(agent);
            }
        
        }
=======
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
    }

}
