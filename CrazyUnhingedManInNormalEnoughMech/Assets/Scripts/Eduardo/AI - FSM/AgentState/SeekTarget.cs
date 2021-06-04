using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "AI/AgentState/SeekTarget")]
    public class SeekTarget : AgentState
    {
        public override void OnStateEnter(AgentBehaviour agent)
        {
            agent.enableSensor(true);

            if (agent.target == null)
            {
                Debug.Log($"{agent} is in state: {this} and has no target!");
                OnStateExit(agent);
                return;
            }
        }

        public override void Tick(AgentBehaviour agent)
        {
            if (agent.target == null)
            {
                OnStateExit(agent);
            }

            agent.destination = agent.target.transform.position;
            float distance = Vector3.Distance(agent.transform.position, agent.destination);
            Debug.DrawLine(agent.target.transform.position, agent.transform.position, Color.red);

            if (distance > agent.sensor.detection)
                agent.MoveToward(agent.destination);
            else
            {
                agent.enableSensor(true);
                OnStateExit(agent);
            }
        }
    }
}
