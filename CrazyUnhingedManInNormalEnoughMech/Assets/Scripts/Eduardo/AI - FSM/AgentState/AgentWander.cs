using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Make agent wander around the environment 
    /// </summary>
    [CreateAssetMenu(menuName = "AI/AgentState/WanderState")]
    public class AgentWander : AgentState
    {
        #region CONSTRUCTOR
        public AgentWander()
        {

        }
        #endregion

        #region FUNCTIONS
        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.currState = this;
        }

        public override void Tick(AgentBehaviour agent)
        {
            DrawSphere(agent);

        }

        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            throw new System.NotImplementedException();
        }

        private void DrawSphere(AgentBehaviour agent)
        {
            Gizmos.color = Color.yellow;
            // Gizmos.DrawSphere(agent.transform.position, 1);
            Gizmos.DrawWireSphere(agent.transform.position, 2);
        }
        #endregion
    }

}
