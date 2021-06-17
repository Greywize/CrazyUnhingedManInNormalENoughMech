<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;
=======
﻿using UnityEngine;
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4

namespace AI
{
    /// <summary>
    /// Artificial behavioural unit
    /// Finite State Machine
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public class AgentBehaviour : MonoBehaviour
    {
<<<<<<< HEAD
        #region CONTRUCTOR 
=======
        #region CONTRUCTOR

<<<<<<< HEAD
=======
>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
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
        public Transform Body;
       // public Sensor sensor;
        public AgentBehaviour agent;

        [Space(15)] [Header("STATE")] 
        public float timer;
=======
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
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        public AgentState defaultState;
        public int currentTransition;
        public AgentState currState;
        public AgentState prevState;
<<<<<<< HEAD
        public Condition stateCondition;

        [Space(15)] 
        [Header("ACTIONS")]
        [FormerlySerializedAs("currAction")] 
        public int actionIndex;
        [SerializeField] public AgentAction[] agentActions;
        public Condition actionCondition;

        [Space(15)] 
        [Header("DESTINATION")] 
=======
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
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        public AgentBehaviour target;

        public Vector3 currPosition;
        public int currDestination = 0;
        public Vector3 destination;
<<<<<<< HEAD
        [Range(5, 100)] public int moveSpeed = 5;

        [Space(15)]
        [Header("SENSOR")] 
        public SphereCollider SenseCollider;
        public float actionRange;
        public float detectProximity;
        public bool closestTarget = false;
=======
        [Range(5,100)] public int moveSpeed = 5;
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        [Space(10)]
        #endregion

        #region PRIVATE MEMBERS
<<<<<<< HEAD

        [Header("TRANSITION")]
=======
        [Header("State Transitions")]
<<<<<<< HEAD
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        [SerializeField] public Transitions[] Transitions;
=======
        [SerializeField]
        public Transitions[] Transitions;

>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
        #endregion

        #region MONOBEHAVIOUR
<<<<<<< HEAD

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, detectProximity);
        }
        
        // #1
=======

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

>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        private void Awake()
        {
            // sensor = GetComponent<Sensor>();
        }

<<<<<<< HEAD
        // #2
        /// <summary>
        /// Add this agent to the total agent count 
        /// </summary>
        private void OnEnable() => agentCounter++;
        
        /// <summary>
        /// Remove this agent from the totals agent count 
        /// </summary>
        private void OnDisable() => agentCounter--;
        
        // #3
=======
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        private void Start()
        {
            AgentState State = ScriptableObject.Instantiate(defaultState);
            currState = State;
            currState.OnStateEnter(this);
            SenseCollider = GetComponent<SphereCollider>();
            SenseCollider.radius = detectProximity;
        }
<<<<<<< HEAD
        
        //#4 - FIXED UPDATE 
        private void FixedUpdate()
        {
            
        }

        // #5 - Update
        private void Update()
        {
            currPosition = transform.position;
            
            if (currState == null)
                checkTransitions(this);
            else if (agentActions != null)
            {
                Debug.DrawLine(destination, transform.position, Color.blue);
=======

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
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
                currState.Tick(this);
            }
        }

        // #6 - 
        private void LateUpdate()
        {
<<<<<<< HEAD
            // Limit check
            if (actionIndex >= agentActions.Length)
                actionIndex = 0;
=======
            if (currState == null)
                timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                currState = defaultState;
                currState.OnStateEnter(this);
                timer = 0.1f;
            }
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        }
        #endregion

        #region FUNCTIONS

        /// <summary>
        /// Enable the sensor on the Agent
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        public void EnableSensor(bool b)
        {
            // s.enabled = b;
            SenseCollider.enabled = b;
            SenseCollider.isTrigger = b;
        }

        /// <summary>
        /// Change agent state then 
        /// </summary>
        /// <param name="agent"></param>
        private void checkTransitions(AgentBehaviour agent)
        {
            for (int i = currentTransition; i < agent.Transitions.Length; i++)
            {
                if (Transitions[i].condition != null)
                    Transitions[i].checkTransition(agent, Transitions[i].state, Transitions[i].condition);
                else
                {
                    // currState = Transitions[i].state;
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public void ResetAgent(AgentBehaviour agent, AgentState s)
        {
            agent.prevState = s;

            //  if (agent.currState != null)
            agent.currState = null;

            if (agent.target != null)
                agent.target = null;

<<<<<<< HEAD
            if (agent.agentActions.Length > 0)
            {
                agent.actionIndex = 0;
                Array.Clear(agent.agentActions, 0, agent.agentActions.Length);
            }

=======
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
            agent.destination = Vector3.zero;
        }

        public void ResetTransition(AgentBehaviour agent)
        {
            agent.currentTransition++;

            if (agent.currentTransition >= agent.Transitions.Length)
            {
                agent.currentTransition = 0;
            }
        }

<<<<<<< HEAD
        public void resetAction(AgentBehaviour agent)
        {
            IdleAction act = ScriptableObject.CreateInstance<IdleAction>();
            agent.agentActions[0] = act;
            agent.actionIndex = 0;
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
=======
        private void checkStateType(AgentState t1)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        {
            if (t1.GetType() == typeof(PatrolState))
            {
                Debug.Log($"The current state of the agent is {agent.currState}");
            }
<<<<<<< HEAD

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
            if (agentActions == null && actionIndex == 0)
=======
        }
<<<<<<< HEAD
=======

        private void performActions()
        {
            if (agentActions == null && currAction == 0)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
                return;
            
            if (agentActions.Length < 0)
<<<<<<< HEAD
            {
                agentActions[actionIndex].performAction(this, target);
            }

            actionIndex++;

            // Limit check
            if (actionIndex > agentActions.Length)
                actionIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="actionIndex"></param>
        public void removeAction(AgentBehaviour agent, int actionIndex)
        {
            agent.agentActions[actionIndex] = null;
            agent.actionIndex++;

            // Limit check
            if (this.actionIndex > agentActions.Length)
                this.actionIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="countdown"></param>
        public void resetBehaviour(AgentBehaviour agent, float countdown)
        {
            agent.timer -= Time.deltaTime;

            if (agent.timer <= 0f)
            {
                if (agent.currState != null)
                    agent.currState.OnStateExit(agent);
                else
                {
                    agent.currState = agent.defaultState;
                    agent.agent.currState.OnStateEnter(agent);
                }

                agent.timer = countdown;
            }
        }
        
        #endregion
        
         #region SENSOR
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool getAgent(Collider c)
        {
            return (c.GetComponent<AgentBehaviour>());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
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
        
=======
                agentActions[currAction].performAction(this, target);
            
            currAction++;

            if (currAction > agentActions.Length)
                currAction = 0;
        }

>>>>>>> parent of bf8c73b ([Spider functional] Major refactor: AgentConditions, AgentState, AgentActions)
>>>>>>> af34cea9e93e08ee2efab9b93d0052f1cdb96dc4
        #endregion
    }
}