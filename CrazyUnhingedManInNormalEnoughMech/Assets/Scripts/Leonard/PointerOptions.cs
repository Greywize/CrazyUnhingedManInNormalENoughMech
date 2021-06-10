using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOptions : MonoBehaviour
{
    public Transform pointer;
    private Transform playerTransform;
    private float initialBoxSpeed = 0;
    private float initialMoveSpeed = 0;
    public float boxSpeed = 10.0f;
    public float moveSpeed = 3;
    private LineRenderer lineRenderer;
    private int toggleOptions = 1;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialBoxSpeed = boxSpeed;
        initialMoveSpeed = moveSpeed;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Ray ray = new Ray(pointer.position, pointer.forward);
        RaycastHit hit;
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + 100 * ray.direction);

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (toggleOptions == 1 || toggleOptions == 2)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Rigidbody body = hit.collider.GetComponent<Rigidbody>();

                    if (body)
                    {
                        body.AddForce(10.0f * ray.direction * (toggleOptions == 1 ? 1 : -1));
                    }
                }
            }
            if (toggleOptions == 0)
            {
                Vector3 fwd = ray.direction;
                fwd.y = 0;
                fwd.Normalize();
                playerTransform.position += fwd * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void ChangeToggleOption(int num)
    {
        toggleOptions = num;
    }
    public void ChangeMoveSpeed(int num)
    {
        moveSpeed = initialMoveSpeed * (num + 1);
    }
    public void ChangeBoxSpeed(int num)
    {
        boxSpeed = initialBoxSpeed * (num + 1);
    }
}
