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
            target = agent.target.GetComponent<CharStats>();

            if (agent.target != null)
            {

                if (target == null)
                {
                    Debug.Log($"{name} has attacked {agent.target.name} : No CharStats implemented");
                    onExit(agent);
                }
                else if (target)
                {
                    Debug.Log($" {name} has attacked {agent.target.name} using {_animator.name}");
                    target.takeDMG(agent.GetComponent<CharStats>().getCharDamage());
                    agent.cooldown = 1.0f;
                }
            }
            else
            {
                agent.cooldown = 1.0f;
                onExit(agent);
            }
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent)
        {
            agent.cooldown--;

            if (agent.cooldown <= 0)
                if (_condition)
                    performAction(agent);
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            AttackAction act = ScriptableObject.Instantiate(this);
            agent.ActionList[index] = act;
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
            agent.removeAction(agent, agent.actionIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            if (_condition.CheckCondition(agent))
            {
                Debug.Log($"{agent} has performed {this}");
            }
        }

        public void attackPlayer(AgentBehaviour agent)
        {

        }
    }
}