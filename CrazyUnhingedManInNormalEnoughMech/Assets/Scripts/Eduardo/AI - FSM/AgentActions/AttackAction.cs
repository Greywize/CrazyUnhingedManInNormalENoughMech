using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentAction/AttackAction")]
    public class AttackAction : AgentAction
    {
        public override void performAction(AgentBehaviour agent)
        {
<<<<<<< HEAD
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (_condition)
                performAction(agent);
        }

        public override void addAction(AgentBehaviour agent, int index)
=======
            // TODO
            // Find agent animator
              
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        private void addAction(AgentBehaviour agent)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
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
<<<<<<< HEAD
            agent.removeAction(agent, agent.actionIndex);
=======
            agent.agentActions[agent.currAction] = null;
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
<<<<<<< HEAD
            if (_condition.CheckCondition(agent))
=======
            if (checkCondition(agent, condition))
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
            {
                addAction(agent);
            }
        }
    }
}