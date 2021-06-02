using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zacks.Terrain;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How fast the player moves")]
    float speed = 10;
    [SerializeField]
    [Tooltip("How fast the player falls")]
    float gravity = 10;
    [SerializeField]
    [Tooltip("Amount the player turns when the button to turn is pressed")]
    float turnAmount = 15;
    [SerializeField]
    [Tooltip("Reference to the terrain that was generated")]
    CubeGenerator terrain;
    [SerializeField]
    [Tooltip("Reference to the VR controller")]
    Transform pointer;
    CharacterController cc;
    Vector3 velocity;
    LineRenderer lineRenderer;
    bool touchpadPressed = false;
    public int weaponSwitch;
    public int projectileSpeed = 50;
    public float fireRate = 0.1f;
    private float fireTime;
    public GameObject[] weaponPrefabs;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        fireTime = 0;

        if (terrain)
        {
            Vector3 spawnPos = terrain.GridToWorld(new Vector2Int(terrain.width / 2, terrain.length / 2));
            spawnPos.y += 2;
            transform.position = spawnPos;
        }
    }

    void Update()
    {
        fireTime += Time.deltaTime;
        Shoot();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"The players position in grid coords is {terrain.WorldToGrid(transform.position)}");
        }
    }

    void Shoot()
    {
#if !UNITY_EDITOR
        Ray ray = new Ray(pointer.position, pointer.forward);
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + 5 * ray.direction);

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            FireWeapon(weaponSwitch, ray);
        }
        if (OVRInput.Get(OVRInput.Button.Back))
        {
            weaponSwitch++;
        }
#else
        if (Input.GetMouseButton(0)) // Left click
        {
            FireWeapon(weaponSwitch);
        }
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            weaponSwitch++;
        }
#endif
    }
    private void FireWeapon(int w, Ray ray)
    {
#if !UNITY_EDITOR
        if (weaponSwitch < weaponPrefabs.Length)
        {
            if (fireTime >= fireRate)
            {
                fireTime = 0;
                Rigidbody clone;
                clone = Instantiate(weaponPrefabs[w].GetComponent<Rigidbody>(), ray.origin + 5 * ray.direction, transform.rotation);
                clone.velocity = ray.direction * projectileSpeed;
            }
        }
        else
        {
            weaponSwitch = 0;
        }

        print($"Firing {weaponPrefabs[w]}");
#endif
    }

    private void FireWeapon(int w)
    {
#if UNITY_EDITOR
        if (weaponSwitch < weaponPrefabs.Length)
        {
            if (fireTime >= fireRate)
            {
                fireTime = 0;
                Rigidbody clone;
                clone = Instantiate(weaponPrefabs[w].GetComponent<Rigidbody>(), transform.position + 5 * transform.forward, transform.rotation);
                clone.velocity = transform.forward * projectileSpeed;
            }
        }
        else
        {
            weaponSwitch = 0;
        }

        print($"Firing {weaponPrefabs[w]}");
#endif
    }
    void Move()
    {
#if !UNITY_EDITOR
        Vector3 forward = transform.forward;
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
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && !touchpadPressed)
        {
            transform.eulerAngles += Vector3.up * touchpad.x * turnAmount;
            touchpadPressed = true;
        }
        else if (!OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && touchpadPressed)
        {
            touchpadPressed = false;
        }

        velocity = transform.forward * touchpad.y * speed;
#else
        if (Input.GetKeyDown(KeyCode.A) && !touchpadPressed)
        {
            transform.eulerAngles -= Vector3.up * turnAmount;
            touchpadPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && !touchpadPressed)
        {
            transform.eulerAngles += Vector3.up * turnAmount;
            touchpadPressed = true;
        }
        else
        {
            touchpadPressed = false;
        }

        velocity = transform.forward * Input.GetAxis("Vertical") * speed;
        //new Vector3(0, cc.isGrounded ? 0 : -gravity, Input.GetAxis("Vertical") * speed);
#endif
        velocity.y = cc.isGrounded ? 0 : -gravity;
        cc.Move(velocity * Time.deltaTime);
    }
}
