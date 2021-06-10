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
            if (agent.target != null)
            {
                Debug.Log($"{agent} already has a target: {agent.target} : Condition = {this}");
                return false;
            }

            agent.enableSensor(true);

            // [2] if a target has been found
            if (Vector3.Distance(agent.transform.position, agent.destination) > agent.sensor.detectRadius)
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
