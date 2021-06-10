using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zacks.Misc
{
    public class SpiderMove : MonoBehaviour
    {



        [Header("Walking Movement")]
        public GameObject[] targets;

        [Tooltip("The x and z coords of where the legs should snap back to when too far away (y is calculated)")]
        public Vector3[] localOrigins;

        public int[] oppositeLegs;

        [Space]

        public float stepDistance;


        //Spider balancing related
        int mask;




        // Start is called before the first frame update
        void Start()
        {
            //used for layermask, so raycast only searches for layered ground
            mask = 1 << 8;

            for (int i = 0; i < targets.Length; i++)
            {
                //recalculate where this target should be so the leg snaps back
                RecalculateStep(i, false);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {

            //Update any legs too far from the origin
            for (int i = 0; i < targets.Length; i++)
            {
                if (Vector3.Distance(transform.position, targets[i].transform.position) > stepDistance)
                {
                    //recalculate where this target should be so the leg snaps back
                    RecalculateStep(i, false);
                }
            }
        }

        void RecalculateStep(int index, bool requireOppositeLeg)
        {
            //cast a ray locally down from localOrigions pos

            //place the target at that rays hit point

            if (localOrigins[index] == null)
            {
                Debug.LogError($"Local Origins doesn't have an index of {index}");
                return;
            }

            //set origin of ray to be at offset from player
            Vector3 rayStart = transform.position;

            rayStart += transform.forward * localOrigins[index].z + transform.right * localOrigins[index].x + transform.up * localOrigins[index].y;

            RaycastHit hit;

            //cast a ray locally down
            if (Physics.Raycast(rayStart, transform.up * -1, out hit, 200, mask))
            {
                //l is our legs target
                Lerp l = targets[index].GetComponent<Lerp>();

                if (l == null)
                    Debug.LogError("This target does not have Lerp");

                if (requireOppositeLeg)
                {
                    //if the opposite leg to this one doesnt exist
                    if (oppositeLegs[index] == -1)
                    {
                        //we can move like normal
                        l.secondaryTarget = hit.point;
                    }
                    else
                    {
                        //if the opposite leg is out of range
                        if (oppositeLegs[index] < 0 || oppositeLegs[index] >= targets.Length)
                        {
                            Debug.LogError("Index to move leg to out of range");

                            l.secondaryTarget = hit.point;

                            return;
                        }

                        Lerp oppositeL = targets[oppositeLegs[index]].GetComponent<Lerp>();

                        if (oppositeL == null)
                            Debug.LogError("Opposite legs target does not have a thing");

                        //if the opposite leg is on the ground
                        if (oppositeL.CloseEnough())
                        {
                            l.secondaryTarget = hit.point;
                        }
                    }
                }
                else
                {
                    l.secondaryTarget = hit.point;

                }


            }
        }
    }
}
