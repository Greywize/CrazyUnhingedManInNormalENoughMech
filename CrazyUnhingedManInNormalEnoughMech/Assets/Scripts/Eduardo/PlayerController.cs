using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public float rotateSpeed;
    public int weaponSwitch;
    public int projectileSpeed;
    public GameObject[] weaponPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        weaponSwitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        EditorControls(true);
#else
        VR_Controls();
#endif
    }

    public void EditorControls(bool b)
    {
        if (Input.GetKey(KeyCode.W))
            moveForward();

        if (Input.GetKey(KeyCode.S))
            moveBackward();

        if (Input.GetKey(KeyCode.A))
            moveLeft();

        if (Input.GetKey(KeyCode.D))
            moveRight();

        if (Input.GetMouseButtonDown(0)) // Left click
            if (weaponSwitch < weaponPrefabs.Length)
                FireWeapon(weaponSwitch);
            else
                weaponSwitch = 0;

        if (Input.GetMouseButtonDown(1)) // Right click
            weaponSwitch++;
    }

    private void VR_Controls()
    {
        VR_MovementControls();
        VR_weaponControls();
    }
    public void moveForward()
    {
        player.Translate(player.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    public void moveBackward()
    {
        player.Translate(-player.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    public void moveLeft()
    {
        player.Translate(-player.right * moveSpeed * Time.deltaTime, Space.World);
    }

    public void moveRight()
    {
        player.Translate(player.right * moveSpeed * Time.deltaTime, Space.World);
    }

    private void VR_weaponControls()
    {
        Rigidbody clone;
        Projectile projectile;

        if (OVRInput.Get(OVRInput.Button.Back))
            weaponSwitch++;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            FireWeapon(weaponSwitch);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="w"> weapon number </param>
    private void FireWeapon(int w)
    {
        print($"Firing {weaponPrefabs[w]}");

        if (weaponSwitch < weaponPrefabs.Length)
        {
            Rigidbody clone;
            clone = Instantiate(weaponPrefabs[w].GetComponent<Rigidbody>(), player.position, player.rotation);
            clone.velocity = transform.TransformDirection(player.forward * 50);
        }
        else
        {
            weaponSwitch = 0;
        }
    }

    private void ForcePush()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Rigidbody body = hit.collider.GetComponent<Rigidbody>();
            if (body)
                body.AddForce(100.0f * ray.direction);
        }
    }

    /// <summary>
    /// Movement for VR Controls 
    /// https://docs.unity3d.com/ScriptReference/Vector2.html 
    /// </summary>
    private void VR_MovementControls()
    {
        Vector2 Dpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if (Dpad == new Vector2(0, 1)) // 
            moveForward();

        if (Dpad == new Vector2(0, -1))
            moveBackward();

        if (Dpad == new Vector2(-1, 0))
            moveLeft();

        if (Dpad == new Vector2(1, 0))
            moveRight();
    }
}


