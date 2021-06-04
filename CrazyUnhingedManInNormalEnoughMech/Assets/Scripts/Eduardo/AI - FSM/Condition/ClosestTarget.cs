using UnityEngine;

namespace AI
{

    [CreateAssetMenu(menuName = "AI/Condition/ClosestTarget")]
    public class ClosestTarget : Condition
    {
        #region CONSTRUCTOR 

        #endregion

        #region PUBLIC MEMBERS
        #endregion

        #region PRIVATE MEMBERS

        #endregion

        #region PUBLIC FUNCTIONS
        public override bool CheckCondition(AgentBehaviour agent)
        {
            if (agent.destination == null)
                return false;

            // [2] if a target has been found
            if (Vector3.Distance(agent.transform.position, agent.destination) > 1.2f)
            {
                agent.sensor.closestTarget = false;
                return true;
            }

            return false;
        }
        #endregion

        #region PRIVATE FUNCTIONS

        #endregion
    }


}
