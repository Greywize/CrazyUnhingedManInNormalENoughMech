using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLocomotion : MonoBehaviour
{
    public Transform cam;
    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.eulerAngles.x > 10 && cam.eulerAngles.x > 30)
        {
            Vector3 fwd = cam.forward;
            fwd.y = 0;
            transform.position += fwd * speed * Time.deltaTime;
        }
    }
}
