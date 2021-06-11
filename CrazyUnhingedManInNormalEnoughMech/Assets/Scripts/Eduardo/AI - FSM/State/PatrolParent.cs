using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public class PatrolParent : MonoBehaviour
    {

        #region CONSTRUCTOR
        public PatrolParent()
        {

        }
        #endregion

        #region PUBLIC MEMBERS
        [SerializeField] public PatrolPoint[] PatrolPoints;
        #endregion
        
        #region PRIVATE MEMBERS

        #endregion

        #region MONOBEHAVIOURS
        private void Awake()
        {
            PatrolPoints = GetComponentsInChildren<PatrolPoint>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
    }
}

