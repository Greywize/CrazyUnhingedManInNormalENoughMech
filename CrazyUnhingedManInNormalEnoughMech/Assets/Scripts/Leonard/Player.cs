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
    //public Weapon currentWeapon;
    public GameObject currentWeaponMono;
    public Camera VRCam;
    private float fireTime;
    public GameObject model;
    public int layer = 5;

    void Start()
    {
        if (model)
        {
            model.SetActive(false);
        }

        cc = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        fireTime = 0;
    }

    void Update()
    {
        if (terrain && terrain.done)
        {
            if (model)
            {
                model.SetActive(true);
            }

            cc.enabled = false;
            Vector3 spawnPos = terrain.GridToWorld(new Vector2Int(terrain.width / 2, terrain.length / 2));
            spawnPos.y += 2.1f;
            transform.position = spawnPos;
            cc.enabled = true;
        }

        fireTime += Time.deltaTime;
        SwapWeapon();
        Shoot();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"The players position in grid coords is {terrain.WorldToGrid(transform.position)}");
        }
    }

    void SwapWeapon()
    {
#if !UNITY_EDITOR
        Ray ray = new Ray(VRCam.transform.position, VRCam.transform.forward);
        int layerMask = 1 << layer;

        if (OVRInput.Get(OVRInput.Button.Back) && Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            WeaponHolder newWeapon = hit.collider.GetComponent<WeaponHolder>();

            if (newWeapon)
            {
                currentWeaponMono = newWeapon.weaponMono;
            }
        }
#endif
    }

    void Shoot()
    {
#if !UNITY_EDITOR
        Ray ray = new Ray(pointer.position, pointer.forward);
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + 3 * ray.direction);

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            FireWeapon(ray);
        }
#else
        if (Input.GetMouseButton(0))
        {
            FireWeapon();
        }
#endif
    }

    private void FireWeapon(Ray ray)
    {
#if !UNITY_EDITOR
        if (currentWeaponMono)
        {
            if (fireTime >= currentWeaponMono.GetComponent<WeaponMono>().fireRate)
            {
                fireTime = 0;
                GameObject clone = Instantiate(currentWeaponMono.GetComponent<WeaponMono>().projectile, ray.origin + 3 * ray.direction, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = ray.direction * currentWeaponMono.GetComponent<WeaponMono>().projectileSpeed;
            }
        }
#endif
    }

    private void FireWeapon()
    {
#if UNITY_EDITOR
        if (currentWeaponMono)
        {
            if (fireTime >= currentWeaponMono.GetComponent<WeaponMono>().fireRate)
            {
                fireTime = 0;
                GameObject clone = Instantiate(currentWeaponMono.GetComponent<WeaponMono>().projectile, transform.position + 5 * transform.forward, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = transform.forward * currentWeaponMono.GetComponent<WeaponMono>().projectileSpeed;
            }
        }

        print($"Firing {currentWeaponMono.GetComponent<WeaponMono>().ToString()}");
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
            if (touchpad.y < 0.35)
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
            if (touchpad.y > -0.35)
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
            if (touchpad.x < 0.35)
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
            if (touchpad.x > -0.35)
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
#endif
        velocity.y = cc.isGrounded ? 0 : -gravity;
        cc.Move(velocity * Time.deltaTime);
    }
}
