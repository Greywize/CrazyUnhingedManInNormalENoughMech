using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeout;

    void Start()
    {
        Destroy(gameObject, timeout);
    }
}
