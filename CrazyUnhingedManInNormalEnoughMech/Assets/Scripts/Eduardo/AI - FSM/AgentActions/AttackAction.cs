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
        

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent)
        {
            if(condition)
                performAction(agent);
        }

        public override void addAction(AgentBehaviour agent,int index)
        {
            AttackAction act = ScriptableObject.Instantiate(this);
            agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            AttackAction act = ScriptableObject.Instantiate(this);
            return act;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public override void performAction(AgentBehaviour agent, AgentState target)
        {
            Debug.Log($"{agent} has {this} to {target}");
            agent.removeAction(agent, agent.currAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            if (condition.CheckCondition(agent))
            {
                Debug.Log($"{agent} has performed {this}");
            }
        }
    }
}
