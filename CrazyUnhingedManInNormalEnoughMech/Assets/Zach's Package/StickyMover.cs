using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zacks.Terrain;

namespace Zacks.Misc
{
    [RequireComponent(typeof(CharacterController))]
    public class StickyMover : MonoBehaviour
    {
        public bool stickToSurface = true;

        [Header("Main Body Movement")]
        public float moveSpeed = 10;

        public float rotateSpeed = 90;

        [Space]

        public float angleFactor = 45;

        CharacterController cc;


        Vector3 backLeft;
        Vector3 backRight;
        Vector3 frontLeft;
        Vector3 frontRight;

        RaycastHit lr;
        RaycastHit rr;
        RaycastHit lf;
        RaycastHit rf;

        Vector3 upDir;

        //Spider balancing related
        int mask;

        // Start is called before the first frame update
        void Start()
        {
            cc = GetComponent<CharacterController>();

            mask = 1 << 8;

            // CubeGenerator generator = GameObject.FindGameObjectWithTag("Terrain").GetComponent<CubeGenerator>();

            //Vector3 spawnPos = generator.GridToWorld(new Vector2Int(generator.width / 2 - 5, generator.length / 2 - 5));
           // spawnPos.y += 2;
           // transform.position = spawnPos;
        }

        // Update is called once per frame
        void Update()
        {
            //move locally forward
            cc.Move(transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

            //rotate player
            transform.Rotate(transform.up * rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

            if (!stickToSurface)
                return;

            //calculate what rotation the main body should have based on 4 rays cast underneath us
            backLeft = transform.position + new Vector3(-0.5f, -0.5f, -0.5f);
            backRight = transform.position + new Vector3(0.5f, -0.5f, -0.5f);
            frontLeft = transform.position + new Vector3(-0.5f, -0.5f, 0.5f);
            frontRight = transform.position + new Vector3(0.5f, -0.5f, 0.5f);

            Physics.Raycast(backLeft + Vector3.up, Vector3.down, out lr, Mathf.Infinity, mask);
            Physics.Raycast(backRight + Vector3.up, Vector3.down, out rr, Mathf.Infinity, mask);
            Physics.Raycast(frontLeft + Vector3.up, Vector3.down, out lf, Mathf.Infinity, mask);
            Physics.Raycast(frontRight + Vector3.up, Vector3.down, out rf, Mathf.Infinity, mask);
            upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                     Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                     Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                     Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                    ).normalized;

            //calculate the rotation we want to go towards using the raycast hit normal
            Vector3 newForward = Vector3.ProjectOnPlane(transform.forward, upDir);
            Quaternion desiredRotation = Quaternion.LookRotation(newForward, upDir);

            //angle is an optional step for rotation smoothing, feel free to ignore it and just use your rotateSpeed for step
            float angle = Quaternion.Angle(transform.rotation, desiredRotation);
            float step = rotateSpeed * Mathf.Clamp01(angle / angleFactor);
            //slerp towards the desired rotation at a speed of step
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, step);


#if UNITY_EDITOR
            //Debug lines
            Debug.DrawRay(rr.point, Vector3.up);
            Debug.DrawRay(lr.point, Vector3.up);
            Debug.DrawRay(lf.point, Vector3.up);
            Debug.DrawRay(rf.point, Vector3.up);
#endif
        }
    }
}
