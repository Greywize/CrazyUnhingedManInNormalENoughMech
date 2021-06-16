using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainObject", menuName = "TerrainObject", order = 0)]
public class TerrainObject : ScriptableObject
{
    public GameObject model;
    public float heightOffset = 0f;
    public float scale = 1f;
    [Range(0, 1)]
    public float spawnChance = 0f;
}
