using UnityEngine;

namespace AI
{

    [CreateAssetMenu(menuName = "AI/AgentState/FleeState")]
    public class FleeState : AgentState
    {
        [SerializeField] AgentBehaviour agent;
        [SerializeField] AgentBehaviour target;

        #region SCRIPTABLE OBJECT
        private void Awake()
        {
            
        }
        #endregion

        #region FUNCTIONS
        public override void OnStateEnter(AgentBehaviour agent) 
        {
        
        }
        
        public override void Tick(AgentBehaviour agent) 
        {
        
        }
        
        public override void OnStateExit(AgentBehaviour agent, AgentState s)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
