﻿using UnityEngine;

namespace AI
{
    public class AgentBehaviour : MonoBehaviour
    {
<<<<<<< HEAD
        #region CONTRUCTOR 
=======
        #region CONTRUCTOR

>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        AgentBehaviour()
        {
            agentCounter++;
        }

        #endregion

        #region STATIC MEMBERS
        private static int agentCounter = 0;
        #endregion

        #region PUBLIC MEMBERS
<<<<<<< HEAD
        [Header("Required Objects")]
        public Sensor sensor;
        public AgentBehaviour agent;

        [Space(10)]
        [Header("Agent State")]
        public float timer = 0.1f;
=======

        [Header("Required Objects")] public Sensor sensor;
        public AgentBehaviour agent;

        [Space(10)] [Header("Agent State")] public float timer = 0.1f;
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        public AgentState defaultState;
        public AgentState currState;
        public AgentState prevState;
        public int currentTransition = 0;
<<<<<<< HEAD
        [Space(10)]

        [Header("Agent Actions")]
        public int currAction;
        public AgentAction performAction;
=======
        [Space(10)] [Header("Agent Actions")] public int currAction;
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        public AgentAction[] agentActions;
        [Space(10)]

<<<<<<< HEAD
        [Header("Agent Destination")]
=======
        [Space(10)] [Header("Agent Destination")]
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        public AgentBehaviour target;

        public Vector3 currPosition;
        public int currDestination = 0;
        public Vector3 destination;
        [Range(5,100)] public int moveSpeed = 5;
        [Space(10)]
        #endregion

        #region PRIVATE MEMBERS
        [Header("State Transitions")]
<<<<<<< HEAD
        [SerializeField] public Transitions[] Transitions;
=======
        [SerializeField]
        public Transitions[] Transitions;

>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        #endregion

        #region MONOBEHAVIOUR

        /// <summary>
        /// Add this agent to the total agent count 
        /// </summary>
        private void OnEnable() => agentCounter++;

        /// <summary>
        /// Remove this agent from the totals agent count 
        /// </summary>
        private void OnDisable() => agentCounter--;

        private void OnDrawGizmosSelected()
        {
            // Draw patrol points when agent is selected
/*            if(defaultState.GetType() == typeof(PatrolState))
                foreach(PatrolPoint p in PatrolState)
                {

                }*/
        }

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

<<<<<<< HEAD
            if (agentActions.Length > 0)
                agentActions[0].performAction(this, target);
=======
            if (agentActions != null)
                performActions();
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)

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
                timer = 0.1f;
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
<<<<<<< HEAD
                    transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
=======
                    transform.position =
                        Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
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

            //  if (agent.currState != null)
            agent.currState = null;

/*            if (agent.target != null)
                agent.target = null;*/

            agent.destination = Vector3.zero;
        }

        public void ResetTransition(AgentBehaviour agent)
        {
            agent.currentTransition++;

            if (agent.currentTransition >= agent.Transitions.Length)
            {
                agent.currentTransition = 0;
                /*                agent.currState = agent.defaultState;
                                agent.currState.OnStateEnter(agent);*/
            }
        }

        private void checkStateType(AgentState t1)
        {
            if (t1.GetType() == typeof(PatrolState))
            {
                Debug.Log($"The current state of the agent is {agent.currState}");
            }
        }
<<<<<<< HEAD
=======

        private void performActions()
        {
            if (agentActions == null && currAction == 0)
                return;
            
            if (agentActions.Length < 0)
                agentActions[currAction].performAction(this, target);
            
            currAction++;

            if (currAction > agentActions.Length)
                currAction = 0;
        }

>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        #endregion
    }
}