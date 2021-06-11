using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    /// <summary>
    /// Keeps a the transform.position of the destionation
    /// </summary>
    public class PatrolPoint : MonoBehaviour
    {
        #region CONSTRUCTORS
        public PatrolPoint()
        {
           // point = GetComponent<Transform>().position;
        }
        #endregion

        #region PUBLIC MEMBERS
        public Vector3 point;
        #endregion

        #region MONO BEHAVIOURS
        private void Awake()
        {
            point = GetComponent<Transform>().position;
        }
        #endregion


    }

}
