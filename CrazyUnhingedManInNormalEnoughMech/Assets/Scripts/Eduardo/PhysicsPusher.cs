using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPusher : MonoBehaviour
{
    public Transform pointer;
    private LineRenderer lineRenderer;
    public GameObject sphere;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;

        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + 100 * ray.direction);

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody body = hit.collider.GetComponent<Rigidbody>();
                if (body)
                    body.AddForce(100.0f * ray.direction);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody clone;
            Projectile projectile;

            clone = Instantiate(sphere.GetComponent<Rigidbody>(), pointer.position, pointer.rotation);
            clone.velocity = transform.TransformDirection(pointer.forward * 50);
        }

    }
#else
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody body = hit.collider.GetComponent<Rigidbody>();
                if (body)
                    body.AddForce(100.0f * ray.direction);
            }
        }
    }
#endif
}
