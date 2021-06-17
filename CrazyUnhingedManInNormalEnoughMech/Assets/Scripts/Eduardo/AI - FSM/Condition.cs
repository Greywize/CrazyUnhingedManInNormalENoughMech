using UnityEngine;

namespace AI
{
    public abstract class Condition : ScriptableObject
    {
        #region PROTECTED MEMBERS
        
        #endregion
        
        #region FUNCTIONS
        public abstract bool CheckCondition(AgentBehaviour agent);

        public virtual bool CheckCondition(AgentBehaviour agent, Condition condition)
        {
            return (condition.CheckCondition(agent) ? true : false);
        }

        #endregion

    }
}
