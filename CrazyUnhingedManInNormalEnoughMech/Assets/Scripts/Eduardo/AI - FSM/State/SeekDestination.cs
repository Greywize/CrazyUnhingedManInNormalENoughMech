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
            addActions(agent, _actions);
        }

        /// <summary>
        /// Single purpose state to only seek one destination
        /// </summary>
        /// <param name="agent"></param>
        public override void Tick(AgentBehaviour agent)
        {
            if (agent.actionIndex < agent.ActionList.Length)
                agent.ActionList[agent.actionIndex].Tick(agent);
            else if(agent.actionIndex >= agent.ActionList.Length)
                OnStateExit(agent);
        }

        /// <summary>
        /// Update used when this object is used in a Patrol State
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="ps"></param>
        public override void Tick(AgentBehaviour agent, PatrolState ps)
        {
            agent.destination = destination;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);

            if (distance > 2.0f)
                agent.MoveToward(destination);
            else
                ps.nextDestination(agent, ps);
        }

        /*public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            Array.Clear(agent.ActionList, 0, agent.ActionList.Length);

            for (int i = 0; i < actions.Length; i++)
            {
                agent.ActionList[i] = actions[i].addinstance(agent);
            }
        
        }*/
    }
}
