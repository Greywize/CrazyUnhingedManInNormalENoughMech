using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zacks.Terrain
{
    [RequireComponent(typeof(CubeGenerator))]
    public class ExampleSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("What to spawn on the terrain")]
        GameObject prefabTree = null;

        [SerializeField]
        [Tooltip("A plane of water to spawn in the world")]
        GameObject prefabWater = null;

        [SerializeField]
        [Tooltip("How likely it is to spawn the prefab (0-100)")]
        float chance = 10;

        [Tooltip("Reference to the terrain spawner")]
        CubeGenerator terrain;

        void Awake()
        {
            //Get the terrain
            terrain = GetComponent<CubeGenerator>();

            //Generate a terrain
            terrain.GenerateCubes();

            //If the tree prefab is null then we wont want to spawn trees
            if (prefabTree != null)
            {
                //For each cube in the terrain
                for (int x = 0; x < terrain.width; x++)
                {
                    for (int y = 0; y < terrain.length; y++)
                    {
                        //Get a number between 0 and 99
                        float rand = Random.Range(0, 100);

                        //If the random number is less then the float
                        if (rand < chance)
                        {
                            //Spawn a tree at the current tile
                            //Instantiate(prefabTree, terrain.GridToWorld(terrain.Cubes[x, y]), Quaternion.identity);

                        }
                    }
                }
            }


            //If the prefab for the water is null we won't want to spawn it
            if (prefabWater == null)
                return;

            //Create a position to spawn the water at (need to add half the width and length to x and z coords because the position of the object with CubeGenerator is in the corner)
            Vector3 spawnPos = new Vector3(terrain.transform.position.x + terrain.CalculateWidth() / 2, 1.1f, terrain.transform.position.z + terrain.CalculateLength() / 2);

            //Spawn the water prefab
            GameObject go = Instantiate(prefabWater, spawnPos, Quaternion.identity);

            //set its scale to be the terrains width and length (divide by 10 since this is a plane)
            go.transform.localScale = new Vector3(terrain.CalculateWidth() / 10, 1, terrain.CalculateLength() / 10);

        }

    }
}
