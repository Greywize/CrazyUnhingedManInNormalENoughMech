using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zacks.Misc
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;

        public Transform cameraPos;

        [Space]

        public float moveSpeed;
        public float rotateSpeed;

        private Vector3 velocity;

        // Update is called once per frame
        void Update()
        {
            Vector3 dir = target.position - transform.position;

            // Rotate the camera towards the target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);

            // have the camera's transform move towards the position of cameraPos, another child object of the player
            // SmoothDamp is used to give an effect of speed by having the camera fall slightly behind
            transform.position = Vector3.SmoothDamp(transform.position, cameraPos.position, ref velocity, moveSpeed);
        }
    }
}
