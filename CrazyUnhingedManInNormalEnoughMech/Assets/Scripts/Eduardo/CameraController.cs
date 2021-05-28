using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;
    public float moveSpeed;
    public int weapon; 

    // Start is called before the first frame update
    void Start()
    {
        weapon = 1;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

#else

#endif

    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cam.Translate(cam.up * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            cam.Translate(-cam.up * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            cam.Translate(-cam.right * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            cam.Translate(cam.right * moveSpeed * Time.deltaTime, Space.World);
        }
    }


}
