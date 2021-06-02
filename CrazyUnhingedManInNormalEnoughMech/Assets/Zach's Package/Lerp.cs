using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zacks.Misc
{
    public class Lerp : MonoBehaviour
    {
        public Transform target;

        [HideInInspector]
        public Vector3 secondaryTarget;

        [SerializeField]
        private float yOffSet;

        [SerializeField]
        private float moveSpeed;


        private Vector3 velocity;

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                Vector3 targ = target.position + target.up * yOffSet;

                transform.position = Vector3.SmoothDamp(transform.position, targ, ref velocity, moveSpeed);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, secondaryTarget, ref velocity, moveSpeed);
            }
        }

        public bool CloseEnough()
        {
            return Vector3.Distance(transform.position, secondaryTarget) < 0.5f;
        }
    }
}
