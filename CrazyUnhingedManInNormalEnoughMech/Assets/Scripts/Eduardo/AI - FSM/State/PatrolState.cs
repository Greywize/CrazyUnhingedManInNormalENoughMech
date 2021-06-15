using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/PatrolState")]
    public class PatrolState : AgentState
    {
        #region CONSTRUCTOR
        public PatrolState()
        {

        }
        #endregion

        // [SerializeField] public int DestinationIndex = 0;
        [SerializeField] public int StateWeight = 0;
        [SerializeField] public SeekDestination[] destinations;
        [SerializeField] public PatrolParent patrolParent;

        private void OnEnable()
        {
            
        }

        /// <summary>
        /// Make suer to pass the current agent into the scriptable object to reference it 
        /// </summary>
        /// <param name="agent"></param>
        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.currState = this;
            agent.DestinationIndex = 0;
            patrolParent = agent.GetComponentInChildren<PatrolParent>();
        }

        public override void Tick(AgentBehaviour agent)
        {
            // destinations[DestinationIndex].destination = agent.destination;
            //  if (agent.DestinationIndex < destinations.Length)
            //    destinations[agent.DestinationIndex].Tick(agent, this);

            if (!patrolParent)
            {
                Debug.Log($"no patrolParent found in agent");
                OnStateEnter(agent);
                return;
            }

            if (agent.DestinationIndex < patrolParent.PatrolPoints.Length)
            {
                for (int i = agent.DestinationIndex; i < patrolParent.PatrolPoints.Length; i++)
                {
                    agent.destination = patrolParent.PatrolPoints[i].point;

                    if (Vector3.Distance(agent.transform.position, agent.destination) >= 2.0f)
                        agent.MoveToward(agent.destination);
                    else
                        agent.DestinationIndex++;
                }
            }
        }

        public override void addActions(AgentBehaviour agent, AgentAction[] actions)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Set location towardss the next destination in the patrol path
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public void nextDestination(AgentBehaviour agent, PatrolState s)
        {
            agent.DestinationIndex++;

            if (agent.DestinationIndex >= destinations.Length)
                OnStateExit(agent);
        }

    }
}
