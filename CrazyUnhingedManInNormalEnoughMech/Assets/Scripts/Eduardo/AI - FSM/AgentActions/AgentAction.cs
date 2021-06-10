using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Base class for the Agents Actions
    /// </summary>
    public abstract class AgentAction : ScriptableObject
    {
        [SerializeField] protected Condition _condition;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected Color _color = Color.white;

        public abstract void performAction(AgentBehaviour agent, AgentBehaviour target);

        public virtual void performAction(AgentBehaviour agent, AgentState s) { }

        public abstract void performAction(AgentBehaviour agent);

        public abstract void Tick(AgentBehaviour agent, Condition cond);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Tick(AgentBehaviour agent)
        {
            if(_condition)
                performAction(agent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public virtual void onExit(AgentBehaviour agent)
        {
            agent.agentActions[agent.actionIndex] = null;
            agent.actionIndex++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="index"></param>
        public abstract void addAction(AgentBehaviour agent, int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public abstract AgentAction addinstance(AgentBehaviour agent);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Condition getCondition()
        {
            return _condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="action"></param>
        public virtual void removeAction(AgentBehaviour agent, AgentAction action)
        {
            Debug.Log($"{agent} : removeAction() not implemented {action}");
        }

        public virtual void ExitAction(AgentBehaviour agent, AgentAction action)
        {
            agent.actionIndex++;
        }
    }
}