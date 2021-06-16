using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;

namespace AI
{
    /// <summary>
    /// Artificial behavioural unit
    /// Finite State Machine
    /// </summary>
    [RequireComponent(typeof(SphereCollider), typeof(Animator))]
    public class AgentBehaviour : MonoBehaviour
    {
        #region CONTRUCTOR

        AgentBehaviour()
        {
            // agentCounter++;
        }

        #endregion

        #region STATIC MEMBERS


        #endregion

        #region PUBLIC MEMBERS

        [Header("Required Objects")] 
        public Transform Body;
        public GameManager GM;
        public AgentBehaviour agent;

        [Space(15)] [Header("STATE")] 
        public float timer;
        public AgentState defaultState;
        public int currentTransition;
        public AgentState currState;
        public AgentState prevState;
        public Condition stateCondition;

        [Space(15)] [Header("ACTIONS")] [FormerlySerializedAs("currAction")]
        public int actionIndex;

        [FormerlySerializedAs("agentActions")] 
        [SerializeField] public AgentAction[] ActionList;
        public Condition actionCondition;
        
        [Space(15)] [Header("TRACKING")] 
        public AgentBehaviour target;

        [Space(15)] [Header("DESTINATION")] 
        [FormerlySerializedAs("currDestination")] public int DestinationIndex = 0;
        public Vector3 currPosition;
        public Vector3 destination;
        [Range(5, 100)] public int moveSpeed = 5;

        [Space(15)] [Header("SENSOR")] public SphereCollider SenseCollider;
        public float actionRange;
        public float detectProximity;
        public bool closestTarget = false;

        [Space(10)]

        #endregion

        #region PRIVATE MEMBERS

        [Header("TRANSITION")]
        [SerializeField]
        public Transitions[] Transitions;

        #endregion

        #region MONOBEHAVIOUR

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, detectProximity);
        }

        // #1
        private void Awake()
        {
            // sensor = GetComponent<Sensor>();
        }

        // #2
        /// <summary>
        /// Add this agent to the total agent count 
        /// </summary>
      //  private void OnEnable() => GM.AllAgents.;

        /// <summary>
        /// Remove this agent from the totals agent count 
        /// </summary>
      //  private void OnDisable() => GM.agents.Remove(this);

        // #3
        private void Start()
        {
            AgentState State = ScriptableObject.Instantiate(defaultState);
            currState = State;
            currState.OnStateEnter(this);
            SenseCollider = GetComponent<SphereCollider>();
            SenseCollider.radius = detectProximity;
        }

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
            else if (ActionList.Length > 0 || ActionList != null)
            {
                currState.Tick(this);
            }
        }

        // #6 - 
        private void LateUpdate()
        {
 
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
            SenseCollider.enabled = b;
            SenseCollider.isTrigger = b;
        }

        /// <summary>
        /// Change agent state when condition is met
        /// </summary>
        /// <param name="agent"></param>
        public void checkTransitions(AgentBehaviour agent)
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

            if (Vector3.Dot(direction, destination) >= 0.2f)
            {
                // https://stuartspixelgames.com/2018/06/21/move-an-object-towards-a-target-in-unity/
                RotateToTarget();

                Body.position =
                    Vector3.MoveTowards(Body.position, destination, moveSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Return the normal of the direction the agent is travelling
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
        public void RotateToTarget()
        {
            Vector3 targetDirection = GetDirection(agent.destination);
            float singleStep = moveSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(Body.forward, targetDirection, singleStep, 0.0f);
            Body.rotation = Quaternion.LookRotation(newDirection);
            // https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="s"></param>
        public void ResetState(AgentBehaviour agent, AgentState s)
        {
            agent.prevState = s;
            agent.currState = null;
            agent.destination = Vector3.zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        public void ResetTransition(AgentBehaviour agent)
        {
            if (agent.currentTransition >= agent.Transitions.Length)
                agent.currentTransition = 0;
        }

        /// <summary>
        /// Reset the agents actions if they have all been performed
        /// (actionIndex is out of bounds)
        /// </summary>
        /// <param name="agent"></param>
        public void resetAction(AgentBehaviour agent)
        {
            Array.Clear(agent.ActionList, 0, agent.ActionList.Length);

            IdleAction act = ScriptableObject.CreateInstance<IdleAction>();
            IdleCondition cond = ScriptableObject.CreateInstance<IdleCondition>();

            agent.ActionList[0] = act;
            agent.actionIndex = 0;
            agent.actionCondition = cond;
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
        /// <param name="agent"></param>
        /// <param name="actionIndex"></param>
        public void removeAction(AgentBehaviour agent, int actionIndex)
        {
            agent.ActionList[actionIndex] = null;
            agent.actionIndex++;

            // Limit check
            if (this.actionIndex > ActionList.Length)
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
                //  agent.currentTransition++;
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

        #endregion
    }
}