using UnityEngine;

namespace AI
{
    public class AgentBehaviour : MonoBehaviour
    {
        #region CONTRUCTOR 
        AgentBehaviour()
        {
            agentCounter++;
        }
        #endregion

        #region STATIC MEMBERS
        private static int agentCounter = 0;
        #endregion

        #region PUBLIC MEMBERS
        [Header("Required Objects")]
        public Sensor sensor;
        public AgentBehaviour agent;

        [Space(10)]
        [Header("Agent State")]
        public float timer = 1;
        public AgentState defaultState;
        public AgentState currState;
        public AgentState prevState;
        public int currentTransition = 0;
        [Space(10)]

        [Header("Agent Destination")]
        public AgentBehaviour target;
        public Vector3 currPosition;
        public int currDestination = 0;
        public Vector3 destination;
        public int moveSpeed;
        [Space(10)]
        #endregion

        #region PRIVATE MEMBERS
        [Header("State Transitions")]
        [SerializeField] public Transitions[] Transitions;
        #endregion

        #region MONOBEHAVIOUR
        private void Awake()
        {
            sensor = GetComponent<Sensor>();
        }

        private void Start()
        {
            AgentState State = ScriptableObject.Instantiate(defaultState);
            currState = State;
            currState.OnStateEnter(this);
        }

        private void Update()
        {
            currPosition = transform.position;
            if (currState == null)
                checkTransitions(this);
            else
                currState.Tick(this);
        }

        private void LateUpdate()
        {
            if (currState == null)
                timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                currState = defaultState;
                currState.OnStateEnter(this);
                timer = 1;
            }
        }
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// Enable the senseor on the Agent
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        public void enableSensor(bool b)
        {
            // s.enabled = b;
            sensor.sphereCollider.enabled = b;
            sensor.sphereCollider.isTrigger = b;
        }

        /// <summary>
        /// Change agent state then 
        /// </summary>
        /// <param name="agent"></param>
        private void checkTransitions(AgentBehaviour agent)
        {
            for (int i = currentTransition; i < agent.Transitions.Length; i++)
            {
                Transitions[i].checkTransition(agent, Transitions[i].state, Transitions[i].condition);
            }
        }

        /// <summary>
        /// Move the Agent towards the destination
        /// </summary>
        /// <param name="destination"></param>
        public void MoveToward(Vector3 destination)
        {
            Vector3 direction = GetDirection(destination);
            float distance = Vector3.Distance(transform.position, destination);

            if (Vector3.Dot(direction, destination) >= 0.2f)
            {
                // https://stuartspixelgames.com/2018/06/21/move-an-object-towards-a-target-in-unity/
                lookAtTarget();

                if (distance >= 2.0f)
                    transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Reurn the normal of the direction the agent is travelling
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Vector3 GetDirection(Vector3 destination)
        {
            // To get the direction we need to move in 
            // we need to subtract the destination from our current position 
            return (destination - transform.position).normalized;
        }

        /// <summary>
        /// 
        /// </summary>
        public void lookAtTarget()
        {
            Vector3 targetDirection = GetDirection(agent.destination);
            float singleStep = moveSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            // https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
        }

        public void ResetAgent(AgentBehaviour agent, AgentState s)
        {
            agent.prevState = s;

            if (agent.currState != null)
                agent.currState = null;

            if (agent.target != null)
                agent.target = null;

            // agent.destination = Vector3.zero;
        }

        public void ResetTransition(AgentBehaviour agent)
        {
            agent.currentTransition++;

            if (agent.currentTransition >= agent.Transitions.Length)
            {
                agent.currentTransition = 0;
                agent.currState = agent.defaultState;
                agent.currState.OnStateEnter(agent);
            }
        }

        private void checkStateType(AgentState t1)
        {
            if (t1.GetType() == typeof(PatrolState))
            {
                Debug.Log($"The current state of the agent is {agent.currState}");
            }
        }
        #endregion
    }
}