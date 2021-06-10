using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Checks how far the agent is from the 
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Condition/DistanceFromTarget")]
    public class DistanceFromTarget : MonoBehaviour
    {
        [Range(5, 50)] public float detectRange;
        

    }
}