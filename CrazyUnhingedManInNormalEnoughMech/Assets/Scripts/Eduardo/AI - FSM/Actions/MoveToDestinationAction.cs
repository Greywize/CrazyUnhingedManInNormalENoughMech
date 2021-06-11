using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Move agent to the destination
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Action/MoveToDestinationAction")]
    public class MoveToDestinationAction : AgentAction
    {
        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            throw new System.NotImplementedException();
        }

        public override void performAction(AgentBehaviour agent)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            float distance = Vector3.Distance(agent.transform.position, agent.destination);
            Debug.DrawLine(agent.target.transform.position, agent.transform.position, Color.red);

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
            if (_condition.CheckCondition(agent))
            {
                float distance = Vector3.Distance(agent.transform.position, agent.destination);
                Debug.DrawLine(agent.destination, agent.transform.position, _color);

                if (distance > agent.detectProximity)
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
            MoveToDestinationAction act = ScriptableObject.Instantiate(this);
            agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            MoveToDestinationAction act = ScriptableObject.Instantiate(this);
            return act;
        }
    }
}