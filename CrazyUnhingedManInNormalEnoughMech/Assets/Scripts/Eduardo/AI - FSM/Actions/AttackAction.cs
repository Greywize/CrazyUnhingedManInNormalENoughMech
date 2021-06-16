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
            // Implement Animator 
            _animator = agent.GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.Log($"{name} has attacked {agent.target.name} : Animator not implemented");
                onExit(agent);
            }
            else if (_animator)
            {
                Debug.Log($" {name} has attacked {agent.target.name} using {_animator.name}");
                _animator.SetBool("AttackTarget", true);
                onExit(agent);
            }

            if (_cooldown >= 0)
            {
                agent.moveSpeed = 0;
            }
            
            // _animator.SetBool("AttackTarget", false);
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