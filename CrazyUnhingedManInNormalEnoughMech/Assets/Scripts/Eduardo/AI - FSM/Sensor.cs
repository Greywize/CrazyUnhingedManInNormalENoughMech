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

      

      



    }

}
