using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public int projectileSpeed = 50;
    public float fireRate = 0.1f;
    public int invert = 0;
    public Transform transform;
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;

    void Start()
    {
        Transform spawnTransform = transform;
        spawnTransform.localScale = new Vector3(1 + (-2 * invert), 1, 1);
        Instantiate(weaponPrefab, spawnTransform);
    }
}
