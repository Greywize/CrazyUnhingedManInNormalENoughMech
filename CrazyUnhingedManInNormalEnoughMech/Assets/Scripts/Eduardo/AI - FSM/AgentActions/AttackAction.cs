using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentAction/AttackAction")]
    public class AttackAction : AgentAction
    {
        public Animator animator;
        public Condition condition;
        public float cooldown = 2.0f;

        private void Awake()
        {
            // TODO
            // Find agent animator
              
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        private void addAction(AgentBehaviour agent)
        {
<<<<<<< HEAD
            agent.performAction = this;
=======
            /*agent.performAction = this;*/
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public override void performAction(AgentBehaviour agent)
        {
<<<<<<< HEAD
            if (checkCondition(agent, condition))
            {
                agent.performAction = this;
            }
=======
            /*if (checkCondition(agent, condition))
            {
                agent.performAction = this;
            }*/
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public override void performAction(AgentBehaviour agent, AgentState target)
        {
<<<<<<< HEAD
            if (checkCondition(agent, condition))
            {
                addAction(agent);
            }
=======
            Debug.Log($"{agent} has {this} to {target}");
            agent.agentActions[agent.currAction] = null;
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            if (checkCondition(agent, condition))
            {
                addAction(agent);
            }
        }
    }
}
