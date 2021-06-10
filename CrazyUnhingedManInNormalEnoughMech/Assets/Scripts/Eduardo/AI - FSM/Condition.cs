using UnityEngine;

namespace AI
{
    public abstract class Condition : ScriptableObject
    {
        #region FUNCTIONS
        public abstract bool CheckCondition(AgentBehaviour agent);
        #endregion

    }
}
