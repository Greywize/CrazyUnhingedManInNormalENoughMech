using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMono : MonoBehaviour
{
    public int projectileSpeed = 50;
    public float fireRate = 0.1f;
    public int invert = 0;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;

    void Start()
    {
        Transform spawnTransform = transform;
        spawnTransform.localScale = new Vector3(1 + (-2 * invert), 1, 1);
        Instantiate(weaponPrefab, spawnTransform);
    }
}
