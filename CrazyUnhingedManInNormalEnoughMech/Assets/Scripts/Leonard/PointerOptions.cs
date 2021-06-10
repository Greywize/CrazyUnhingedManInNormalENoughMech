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
    public float turnAmount = 15;
    private LineRenderer lineRenderer;
    private int toggleOptions = 0;
    private bool touchpadPressed = false;

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
        Vector2 touchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if (touchpad.y > 0)
        {
            if (touchpad.y < 0.4)
            {
                touchpad.y = 0;
            }
            else
            {
                touchpad.y = 1;
            }
        }
        else if (touchpad.y < 0)
        {
            if (touchpad.y > -0.4)
            {
                touchpad.y = 0;
            }
            else
            {
                touchpad.y = -1;
            }
        }
        if (touchpad.x > 0)
        {
            if (touchpad.x < 0.4)
            {
                touchpad.x = 0;
            }
            else
            {
                touchpad.x = 1;
            }
        }
        else if (touchpad.x < 0)
        {
            if (touchpad.x > -0.4)
            {
                touchpad.x = 0;
            }
            else
            {
                touchpad.x = -1;
            }
        }

        playerTransform.position += playerTransform.forward * touchpad.y * moveSpeed * Time.deltaTime;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && !touchpadPressed)
        {
            playerTransform.eulerAngles += Vector3.up * touchpad.x * turnAmount;
            touchpadPressed = true;
        }
        else if (!OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && touchpadPressed)
        {
            touchpadPressed = false;
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
