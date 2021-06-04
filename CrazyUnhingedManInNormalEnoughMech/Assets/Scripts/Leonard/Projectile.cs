using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float timeout;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeout);
    }
}
