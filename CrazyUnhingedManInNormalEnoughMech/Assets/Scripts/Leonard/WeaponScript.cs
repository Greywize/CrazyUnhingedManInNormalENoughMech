using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public int projectileSpeed = 50;
    public float fireRate = 0.1f;
    public GameObject projectile;
}
