using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "AI/AgentAction/SeekAction")]
    public class SeekAction : AgentAction
    {
        private void OnEnable()
        {
            if (_condition == null)
            {
                Condition defaultReached = ScriptableObject.CreateInstance<DestinationNotReached>();
                _condition = defaultReached;
            }
        }

        private void Awake()
        {
            _condition = ScriptableObject.CreateInstance<DestinationNotReached>();
        }

        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            throw new System.NotImplementedException();
        }

        public override void performAction(AgentBehaviour agent)
        {
            throw new NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            agent.destination = agent.target.transform.position;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);
            Debug.DrawLine(agent.target.transform.position, agent.transform.position, _color);

            if (distance > agent.detectProximity)
                agent.MoveToward(agent.destination);
            else
            {
                agent.removeAction(agent, agent.actionIndex);
                agent.EnableSensor(true);
            }
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (_condition.CheckCondition(agent, _condition))
            {
                if (agent.currState.GetType() == typeof(SeekTarget))
                    agent.destination = agent.target.transform.position;
                
                float distance = Vector3.Distance(agent.transform.position, agent.destination);
                agent.MoveToward(agent.destination);
            }
            else
            {
                agent.actionIndex++;
                agent.EnableSensor(true);
            }
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            SeekAction act = ScriptableObject.Instantiate(this);
            agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            SeekAction act = ScriptableObject.CreateInstance<SeekAction>();
           // Debug.Log($"Seek action: {agent} : {act.GetInstanceID().ToString()}");
            return act;
        }
    }
}