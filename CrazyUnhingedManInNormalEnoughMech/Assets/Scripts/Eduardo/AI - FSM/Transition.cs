using System;
using UnityEngine;

namespace AI
{
    [Serializable]
    public class Transitions
    {
        #region CONSTRUCTOR
        public Transitions(AgentBehaviour agent, AgentState state, Condition condition)
        {
            // this.Agent = agent;
            this.state = state;
            this.condition = condition;
        }
        #endregion

        #region PRIVATE MEMBERS
        #endregion

        #region PUBLIC MEMBERS
        public AgentState state;
        public Condition condition;
        #endregion

        #region FUNCTIONS
        public void checkTransition(AgentBehaviour agent, AgentState s, Condition c)
        {
            Condition condition = ScriptableObject.Instantiate(c);
            AgentState State = ScriptableObject.Instantiate(s);

            if (condition.CheckCondition(agent))
            {
                if (agent.currState != null)
                    agent.currState.OnStateExit(agent);

                agent.currState = State;
                agent.currState.OnStateEnter(agent);
            } 
        }
        #endregion
    }
}
