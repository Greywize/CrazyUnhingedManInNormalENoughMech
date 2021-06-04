using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AgentAction : ScriptableObject
    {
     
        public abstract void performAction(AgentBehaviour agent);
        public abstract void performAction(AgentBehaviour agent, AgentState s);

    }

}

