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

        // [SerializeField] public int currDestination = 0;
        [SerializeField] public int StateWeight = 0;
        [SerializeField] public SeekDestination[] destinations;
        [SerializeField] public PatrolParent patrolParent;


        /// <summary>
        /// Make suer to pass the current agent into the scriptable object to reference it 
        /// </summary>
        /// <param name="agent"></param>
        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.currState = this;
            agent.currDestination = 0;
            patrolParent = agent.GetComponentInChildren<PatrolParent>();
        }

        public override void Tick(AgentBehaviour agent)
        {
            // destinations[currDestination].destination = agent.destination;
            //  if (agent.currDestination < destinations.Length)
            //    destinations[agent.currDestination].Tick(agent, this);

            if(!patrolParent)
            {
                Debug.Log($"no patrolParent found in agent");
                OnStateEnter(agent);
                return;
            }
        

            if (agent.currDestination < patrolParent.PatrolPoints.Length)
            {
                for (int i = agent.currDestination; i < patrolParent.PatrolPoints.Length; i++)
                {
                    agent.destination = patrolParent.PatrolPoints[i].point;
                    if (!agent.reachDest)
                        agent.MoveToward(agent.destination);
                    else
                        agent.currDestination++;

                }
            }

        }

        /// <summary>
        /// Set location towardss the next destination in the patrol path
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public void nextDestination(AgentBehaviour agent, PatrolState s)
        {
            agent.reachDest = false;
            agent.currDestination++;

            if (agent.currDestination >= destinations.Length)
                OnStateExit(agent);
        }


        /*        public override void OnStateExit(AgentBehaviour agent, PatrolState p)
                {
                    nextDestination(agent, p);
                }*/

    }
}
