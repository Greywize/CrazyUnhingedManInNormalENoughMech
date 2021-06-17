using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon System/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public struct Timer
    {
        public Timer(float value)
        {
            reset = (timer = value);
        }
        public float timer { get; set; }
        private float reset { get; set; }

        public void Reset()
        {
            timer = reset;
        }
        public void Set(float newTime)
        {
            timer = (reset = newTime);
        }
    }

    public string      weaponName;     // Display name of the weapon
    public float       fireRate;       // How many rounds are shot per second
    public int         magezineSize;   // How many rounds you can shoot before you have to reload
    public int         roundsLeft;     // Remaining projectiles in magezine
    public float       reloadTime;     // The time it takes to fully reload weapon
    public int         spreadRange;    // The range in degrees that the bullets spread;
    public float       bulletVelocity; // Max projectile speed / velocity

    public bool        canFire;        // Use this to disallow firing for reloading

    public Timer       fireRateTimer;  // A timer to handle our fire rate
    public Timer       reloadTimer;    // A timer to handle our reloading

    public Transform   point;          // Transform that represents where our bullet will spawn from
    public GameObject  projectile;     // Reference to the projectile gameObjet our weapon fires
}