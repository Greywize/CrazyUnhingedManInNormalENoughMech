using System;
using System.Collections;
using System.Collections.Generic;
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
        public Transform Body;
        public Sensor sensor;
        public AgentBehaviour agent;

        [Space(10)]
        [Header("Agent State")] 
        public float timer = 0.1f;
        public AgentState defaultState;
        public AgentState currState;
        public AgentState prevState;
        public int currentTransition = 0;
        
        [Space(10)] 
        [Header("Agent Actions")] 
        public int currAction;
        public AgentAction[] agentActions;

        [Space(10)] 
        [Header("Agent Destination")]
        public AgentBehaviour target;
        public Vector3 currPosition;
        public int currDestination = 0;
        public Vector3 destination;
        [Range(5, 100)] public int moveSpeed = 5;

        [Space(10)]

        #endregion

        #region PRIVATE MEMBERS

        [Header("State Transitions")]
        [SerializeField] public Transitions[] Transitions;

        #endregion

        #region MONOBEHAVIOUR
        /// <summary>
        /// Remove this agent from the totals agent count 
        /// </summary>
        private void OnDisable() => agentCounter--;

        private void OnDrawGizmosSelected()
        {
            // Draw patrol points when agent is selected
            // for(int i = 0; i < defaultState)
            
        }

        // #1
        private void Awake()
        {
            sensor = GetComponent<Sensor>();
        }
        
        // #2
        /// <summary>
        /// Add this agent to the total agent count 
        /// </summary>
        private void OnEnable() => agentCounter++;

        // #3
        private void Start()
        {
            AgentState State = ScriptableObject.Instantiate(defaultState);
            currState = State;
            currState.OnStateEnter(this);
        }
        //#4 - FIXED UPDATE 
        
        // #5 - Update
        private void Update()
        {
            currPosition = transform.position;

            /*if (agentActions != null)
                performActions();*/

            if (currState == null)
                checkTransitions(this);
            else if (agentActions != null)
                currState.Tick(this);
        }

        private void LateUpdate()
        {
            // Limit check
            if (currAction > agentActions.Length)
                currAction = 0;

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
        /// Change agent state when condition is met
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
            float distance = Vector3.Distance(Body.position, destination);

            if (Vector3.Dot(direction, destination) >= 0.2f)
            {
                // https://stuartspixelgames.com/2018/06/21/move-an-object-towards-a-target-in-unity/
                lookAtTarget();

                if (distance >= 2.0f)
                    Body.position =
                        Vector3.MoveTowards(Body.position, destination, moveSpeed * Time.deltaTime);
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
            return (destination - Body.position).normalized;
        }

        /// <summary>
        /// 
        /// </summary>
        public void lookAtTarget()
        {
            Vector3 targetDirection = GetDirection(agent.destination);
            float singleStep = moveSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(Body.forward, targetDirection, singleStep, 0.0f);
            Body.rotation = Quaternion.LookRotation(newDirection);
            // https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
        }

        public void ResetAgent(AgentBehaviour agent, AgentState s)
        {
            agent.prevState = s;

            //  if (agent.currState != null)
            agent.currState = null;

/*            if (agent.target != null)
                agent.target = null;*/

            if (agent.agentActions.Length > 0)
            {
                agent.currAction = 0;
                Array.Clear(agent.agentActions, 0, agent.agentActions.Length);
            }

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

        // TODO - return the current type of the state
        public AgentState getStateType(AgentState t1)
        {
            if (t1.GetType() == typeof(PatrolState))
            {
                Debug.Log($"The current state of the agent is {agent.currState}");
                return t1;
            }
            
            return null;
        }
        
        // TODO - 
        public string checkStateType(AgentState t1)
        {
            if (t1.GetType() == typeof(PatrolState))
            {
                Debug.Log($"The current state of the agent is {agent.currState}");
                return t1.GetType().ToString();
            }
            
            return null;
        }
        
        // TODO - 
        public string checkStateType(AgentAction action)
        {
            // return (action.GetType().ToString() ?
            return null;
        }
    
        /// <summary>
        /// 
        /// </summary>
        private void performActions()
        {
            // Guard clause
            if (agentActions == null && currAction == 0)
                return;

            if (agentActions.Length < 0)
            {
                agentActions[currAction].performAction(this, target);
            }

            currAction++;

            // Limit check
            if (currAction > agentActions.Length)
                currAction = 0;
        }

        public void removeAction(AgentBehaviour agent, int actionIndex)
        {
            agent.agentActions[actionIndex] = null;
            agent.currAction++;
            
            // Limit check
            if (currAction > agentActions.Length)
                currAction = 0;
        }
        
        
        #endregion
    }
}