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
    public float turnSpeed = 3;
    private LineRenderer lineRenderer;
    private int toggleOptions = 0;

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
        lineRenderer.SetPosition(1, ray.origin + 2500 * ray.direction);

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
        }

        Vector3 forward = playerTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 forwardRight = playerTransform.right;
        Vector2 touchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        playerTransform.position += playerTransform.forward * touchpad.y * moveSpeed * Time.deltaTime;
        playerTransform.eulerAngles += Vector3.up * touchpad.x * turnSpeed * Time.deltaTime;
        //playerTransform.forward += new Vector3(forward.x * (turnSpeed / 10) * Time.deltaTime, 0, 0);
        //playerTransform.position += forward * moveSpeed * Time.deltaTime;
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
