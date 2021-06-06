using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public class Sensor : MonoBehaviour
    {
        public AgentBehaviour agent;
        public SphereCollider sphereCollider;
        public float detectRadius;
        public float proximity;
        public bool closestTarget = false;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(agent.transform.position, proximity);
        }

        private void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
            sphereCollider = GetComponent<SphereCollider>();
        }

        // Start is called before the first frame update
        void Start()
        {
            sphereCollider.radius = proximity;
            // StartCoroutine(targetCheck(sensor));
        }

        // Update is called once per frame
        void Update()
        {
            sphereCollider.radius = proximity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (agent.target == null)
            {
                if (getAgent(other))
                {
                    AgentBehaviour enemy = other.GetComponent<AgentBehaviour>();
                    if (!enemy.name.Contains("Seeker"))
                        agent.target = enemy;
                    // agent.target = enemy;
                    //  Debug.DrawLine(enemy.transform.position, agent.transform.position);
                }
            }
            else if (agent.target != null)
            {
                if (getAgent(other))
                {
                    AgentBehaviour enemy = other.GetComponent<AgentBehaviour>();
                    float enemyDist = Vector3.Distance(agent.transform.position, enemy.transform.position);
                    float targetDist = Vector3.Distance(agent.transform.position, agent.target.transform.position);

                    // Seek furtherest target
                    if (closestTarget)
                    {
                        if (enemyDist < targetDist)
                        {
                            if (!enemy.name.Contains("Seeker"))
                                agent.target = enemy;
                        }
                    }
                    else if (!closestTarget)
                    {
                        if (enemyDist > targetDist)
                        {
                            if (!enemy.name.Contains("Seeker"))
                                agent.target = enemy;
                        }
                    }
                }
            }
        }

        private bool getAgent(Collider c)
        {
            return (c.GetComponent<AgentBehaviour>());
        }

    }

}
