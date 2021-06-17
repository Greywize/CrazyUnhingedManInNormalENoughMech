using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public Weapon               weapon;             // Our weapon prefab

    private List<GameObject>    projectileList;     // A list containing the projectiles we need to keep track of

    private void Start()
    {
        projectileList = new List<GameObject>();

        weapon.point = transform.Find("Point").GetComponent<Transform>();

        if (weapon.point == null)
        {
            Debug.LogWarning("GameObject named Point cannot be found as a child of our weapon. Please add one.");
        }

        weapon.fireRateTimer.Set(weapon.fireRate);                                                                      // Setup fire rate timer for the weapon
        weapon.reloadTimer.Set(weapon.reloadTime);
        weapon.fireRateTimer.timer = 0;                                                                                 // Initialse to zero so we can fire our first shot straight away
        weapon.canFire = true;                                                                                          // Set canFire to true on start
    }
    private void Update()
    {
        weapon.fireRateTimer.timer -= Time.deltaTime;                                                                   // Count down timer

        if (weapon.canFire && weapon.roundsLeft > 0)
        {
            if (Input.GetMouseButton(0) && weapon.fireRateTimer.timer <= 0)                                             // If we click to shoot and we're reloaded
            {
                Fire();                                                                                                 // Fire projectile
                weapon.fireRateTimer.Reset();                                                                           // Reset timer
                weapon.roundsLeft--;
            }
        }
        else
        {
            Reload();
        }
    }
    private void FixedUpdate()
    {
        foreach (GameObject projectile in projectileList)                                                           // Handle moving projectiles
        {
            Rigidbody rigidbody = projectile.transform.GetComponent<Rigidbody>();                                   // Get a reference to the rigidbody on our projectile

            rigidbody.AddForce(Vector3.forward * weapon.bulletVelocity, ForceMode.VelocityChange);                  // Move our projectile

            if (rigidbody.velocity.magnitude > weapon.bulletVelocity)                                               // If projectile exceeds maximum velocity
            {
                rigidbody.velocity = Vector3.ClampMagnitude(projectile.transform.forward, weapon.bulletVelocity);   // Clamp velocity
            }
        }
    }
    public void Fire()
    {
        GameObject projectile = Instantiate(weapon.projectile, weapon.point);                                       // Spawn bullet at point
        projectile.transform.parent = null;                                                                         // Remove point as parent of projectile
        projectileList.Add(projectile);                                                                             // Add the projectile to our list
    }

    public void Reload()
    {
        weapon.canFire =    false;     // Disable weapon
        bool reloaded =     false;     // While reloaded is false, this function will continue to be called in Update() so that the timer continues to count down
        if (!reloaded)
        {
            weapon.reloadTimer.timer -= Time.deltaTime;
            if (weapon.reloadTimer.timer <= 0)
            {
                weapon.roundsLeft = weapon.magezineSize;
                weapon.canFire = true;

                weapon.reloadTimer.Reset();
            }
        }
    }
}