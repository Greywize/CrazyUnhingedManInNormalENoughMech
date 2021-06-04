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
    [Tooltip("How fast the player dashes")]
    float dashMultipler = 6;
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
    bool didDash = false;
    bool dashing = false;
    bool touchpadPressed = false;
    //public Weapon[] currentWeapon;
    public List<GameObject> currentWeaponMono = new List<GameObject>();
    public Camera VRCam;
    private float fireTime;
    private float initialDashWarmupTime;
    public float dashWarmupTime = 0.1f;
    private float initialDashTime;
    public float dashTime = 0.65f;
    public float dashCooldownMultiplier = 7;
    public float deadzone = 0.3f;
    public GameObject model;

    void Start()
    {
        if (model)
        {
            model.SetActive(false);
        }

        cc = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();
        fireTime = 0;
        initialDashTime = dashTime;
        initialDashWarmupTime = dashWarmupTime;
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
#if UNITY_EDITOR
        Ray ray = new Ray(VRCam.transform.position, VRCam.transform.forward);
        int layerMask = 1 << 5;

        if (OVRInput.Get(OVRInput.Button.Back) && Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            WeaponHolder newWeapon = hit.collider.GetComponent<WeaponHolder>();

            if (newWeapon)
            {
                int i = 0;

                foreach (GameObject weapon in currentWeaponMono)
                {
                    Destroy(weapon);
                    i++;
                }

                for (int e = 0; e < i; e++)
                {
                    newWeapon.weaponMono.GetComponent<Weapon>().invert = e % 2;
                    currentWeaponMono.Add(Instantiate(newWeapon.gameObject, transform));
                }
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
        if (currentWeaponMono[0])
        {
            if (fireTime >= currentWeaponMono[0].GetComponent<WeaponMono>().fireRate)
            {
                fireTime = 0;
                GameObject clone = Instantiate(currentWeaponMono[0].GetComponent<WeaponMono>().weaponAmmo, ray.origin + 3 * ray.direction, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = ray.direction * currentWeaponMono[0].GetComponent<WeaponMono>().projectileSpeed;
            }
        }
#endif
    }

    private void FireWeapon()
    {
#if UNITY_EDITOR
        if (currentWeaponMono[0])
        {
            if (fireTime >= currentWeaponMono[0].GetComponent<WeaponMono>().fireRate)
            {
                fireTime = 0;
                GameObject clone = Instantiate(currentWeaponMono[0].GetComponent<WeaponMono>().weaponPrefab.GetComponent<Projectile>().projectilePrefab, transform.position + 5 * transform.forward, transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = transform.forward * currentWeaponMono[0].GetComponent<WeaponMono>().projectileSpeed;
            }
        }

        print($"Firing {currentWeaponMono[0].GetComponent<WeaponMono>().ToString()}");
#endif
    }
    void Move()
    {
        velocity = new Vector3();
#if !UNITY_EDITOR
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector2 touchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if (touchpad.y > 0)
        {
            if (touchpad.y < deadzone)
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
            if (touchpad.y > -deadzone)
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
            if (touchpad.x < deadzone)
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
            if (touchpad.x > -deadzone)
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
            if (touchpad.y > touchpad.x && touchpad.y > -touchpad.x)
            {
                if (!dashing)
                {
                    dashing = true;
                }
            }
            else
            {
                transform.eulerAngles += Vector3.up * touchpad.x * turnAmount;
            }

            touchpadPressed = true;
        }
        else if (!OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && touchpadPressed)
        {
            touchpadPressed = false;
        }

#else
        if (Input.GetKeyDown(KeyCode.E) && !touchpadPressed && !dashing)
        {
            dashing = true;
            touchpadPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && !touchpadPressed && (!dashing || dashTime <= 0))
        {
            transform.eulerAngles -= Vector3.up * turnAmount;
            touchpadPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && !touchpadPressed && (!dashing || dashTime <= 0))
        {
            transform.eulerAngles += Vector3.up * turnAmount;
            touchpadPressed = true;
        }
        else
        {
            touchpadPressed = false;
        }
#endif
        float dashCancel = 1;

        if (dashing)
        {
            if (dashWarmupTime >= 0 && dashTime > 0)
            {
                dashCancel = 0;
                dashWarmupTime -= Time.deltaTime;
            }
            else if (dashTime > 0 && !didDash)
            {
                velocity += transform.forward * speed * dashMultipler;
                dashTime -= Time.deltaTime;
            }
            else
            {
                didDash = true;
                dashWarmupTime += Time.deltaTime * (1 / dashCooldownMultiplier);

                if (dashWarmupTime >= initialDashTime)
                {
                    dashTime = initialDashTime;
                    dashWarmupTime = initialDashWarmupTime;
                    didDash = false;
                    dashing = false;
                }
            }
        }
#if !UNITY_EDITOR
        velocity += transform.forward * dashCancel * touchpad.y * speed;
#else
        velocity += transform.forward * dashCancel * Input.GetAxis("Vertical") * speed;
#endif
        velocity.y = cc.isGrounded ? 0 : -gravity;
        cc.Move(velocity * Time.deltaTime);
    }
}
