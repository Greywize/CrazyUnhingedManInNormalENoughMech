using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentAction/SeekDestinationAction")]
    public class SeekDestinationAction : AgentAction
    {
        public Condition condition;

        public override void performAction(AgentBehaviour agent, AgentBehaviour target)
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(AgentBehaviour agent, Condition cond)
        {
            //agent.destination = agent.target.transform.position;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);
            Debug.DrawLine(agent.target.transform.position, agent.transform.position, Color.red);

            if (distance > agent.sensor.detectRadius)
                agent.MoveToward(agent.destination);
            else
            {
                agent.removeAction(agent, agent.currAction);
                agent.enableSensor(true);
            }
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (!condition.CheckCondition(agent))
            {
               // agent.destination = agent.target.transform.position;
                float distance = Vector3.Distance(agent.transform.position, agent.destination);
                Debug.DrawLine(agent.target.transform.position, agent.transform.position, Color.red);

                if (distance > agent.sensor.detectRadius)
                    agent.MoveToward(agent.destination);
            }
            else
            {
                agent.currAction++;
                // agent.removeAction(agent, agent.currAction);
                agent.enableSensor(true);
            }
        }

        public override void addAction(AgentBehaviour agent, int index)
        {
            SeekDestinationAction act = ScriptableObject.Instantiate(this);
            agent.agentActions[index] = act;
        }

        public override AgentAction addinstance(AgentBehaviour agent)
        {
            SeekDestinationAction act = ScriptableObject.Instantiate(this);
            return act;
        }
    }
}