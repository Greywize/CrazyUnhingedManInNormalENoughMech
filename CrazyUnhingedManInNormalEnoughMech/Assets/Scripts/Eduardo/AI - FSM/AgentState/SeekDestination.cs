using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/SeekDestination")]
    public class SeekDestination : AgentState
    {
        [SerializeField] public Vector3 destination;
        public AgentAction[] actions;
        
        /// <summary>
        /// When this state becomes the instantiated behaviour
        /// </summary>
        /// <param name="agent"></param>
        public override void OnStateEnter(AgentBehaviour agent)
        {
            if (agent.currState == null)
                agent.currState = this;

            agent.destination = destination;
            //agent.lookAtTarget();
            addActions(agent, actions);
        }

        /// <summary>
        /// Single purpose state to only seek one destination
        /// </summary>
        /// <param name="agent"></param>
        public override void Tick(AgentBehaviour agent)
        {
            if (agent.currAction < agent.agentActions.Length)
                agent.agentActions[agent.currAction].Tick(agent);
            else
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

        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            SeekDestination act = ScriptableObject.Instantiate(this);
           // agent.agentActions[0] = act;
        }
    }
}
