using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//using Mirror;

namespace Zacks.Terrain
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshBuilder))]
    public class CubeGenerator : MonoBehaviour
    {
        [Header("Spawnable Objects")]
        public TerrainObject[] terrainObjects;
        [Range(1, 20)]
        public float spawnChance = 1f;
        [Space]
        [Header("Size Variables")]
        //[SerializeField]
        [Tooltip("How many cubes wide the terrain will be (Range 1 - inf)")]
        public int width = 50;
        //[SerializeField]
        [Tooltip("How many cubes long the terrain will be (Range 1 - inf)")]
        public int length = 50;
        ///// <summary>
        ///// Returns how many cubes wide the tile is (Use CalculateWidth() for how many world units the terrain is)
        ///// </summary>
        ///// <returns></returns>
        //public int GetWidth() { return width; }
        ///// <summary>
        ///// Returns how many cubes long the tile is (Use CalculateLength() for how many world units the terrain is)
        ///// </summary>
        ///// <returns></returns>
        //public int GetLength() { return length; }
        [Space]
        //[SerializeField]
        [Tooltip("The distance between each cube (default = 0)")]
        public float cubeDistance = 1;
        //[SerializeField]
        [Tooltip("How large each cube is (default = 0.1) (cannot be less than or equal to 0)")]
        public float cubeScale = 0.1f;
        [Header("Perlin Noise Variables")]
        //[SerializeField]
        [Tooltip("The scale of the perlin noise (higher value means more noise)")]
        public float scale = 20;
        [Space]
        //[SerializeField]
        [Tooltip("Raise the cubes height to this power (default 1)")]
        public float power = 1;
        [Space]
        [SerializeField]
        [Tooltip("How high the height should be multiplied (default = 1)")]
        float heightMultiplier = 1;
        //[SerializeField]
        [Tooltip("The offset of the perlin noise generator in the x direction")]
        float offsetX = 100.0f;
        //[SerializeField]
        [Tooltip("The offset of the perlin noise generator in the z direction")]
        float offsetZ = 100.0f;
        //[Tooltip("The offset of the perlin noise generator in the x direction")]
        public float OffSetX
        {
            get { return offsetX; }
            set { offsetX = value; }
        }
        public float OffSetZ
        {
            get { return offsetZ; }
            set { offsetZ = value; }
        }
        [Space]
        //[SerializeField]
        [Tooltip("Whether to round the cubes y position to an int")]
        public bool roundToInt = false;
        [Header("Material Variables")]
        //[SerializeField]
        [Tooltip("The color of the tiles as the height of the terrain increases")]
        public Gradient gradient = null;
        MeshBuilder meshBuilder;
        //all of the cubes in our terrain
        Vector3[,] m_cubes;
        public Vector3[,] Cubes
        {
            get { return m_cubes; }
        }
        float[,] perlinVals;
        [Header("Other Options")]
        [SerializeField]
        [Tooltip("When true, allows larger meshes to be generated (WARNING: meshes large enough to need this will affect performance)")]
        bool allocateLargerMemory = false;
        [Space]
        //[SerializeField]
        [Tooltip("The minimum height difference they need in order to have a triangle drawn between them")]
        public float heightDifMin = 0.01f;
        [Space]
        [SerializeField]
        [Tooltip("Whether the mesh should generate when the game starts")]
        bool generateOnAwake = false;
        public bool done = false;
        //makes sure the perlin noise isnt passed in integer form
        const float refiner = 1.1f;
        /// <summary>
        /// How many world units wide the terrain is
        /// </summary>
        /// <returns></returns>
        public float CalculateWidth() { return width * (cubeScale + cubeDistance); }
        /// <summary>
        /// How many world units long the terrain is
        /// </summary>
        /// <returns></returns>
        public float CalculateLength() { return length * (cubeScale + cubeDistance); }

        private void Awake()
        {
            if (generateOnAwake)
            {
                GenerateCubes();
            }
        }

        void Update()
        {
            done = meshBuilder.done;

            if (done)
            {
                meshBuilder.done = false;
            }
        }

        /// <summary>
        /// Helper function that starts the coroutine generateCubes
        /// </summary>
        public void GenerateCubes(Vector2 seed = default(Vector2), GameObject loadingPanel = null, 
            TextMeshProUGUI specifcLoadingText = null, TextMeshProUGUI loadingPercent = null, Slider loadingBar = null)
        {
            StartCoroutine(generateCubes(seed, loadingPanel, specifcLoadingText, loadingPercent, loadingBar));
        }

        /// <summary>
        /// Creates the terrain from a passed in seed (random seed if blank)
        /// </summary>
        public IEnumerator generateCubes(Vector2 seed = default(Vector2), GameObject loadingPanel = null, 
            TextMeshProUGUI specifcLoadingText = null, TextMeshProUGUI loadingPercent = null, Slider loadingBar = null)
        {
            meshBuilder = GetComponent<MeshBuilder>();

            m_cubes = new Vector3[width, length];
            perlinVals = new float[width, length];

            if (!meshBuilder)
                Debug.LogError($"{this.gameObject.name} does not have a MeshBuilder Component, please add one.");

            if (seed == new Vector2(0, 0))
            {
                RandomiseArea();
            }
            else
            {
                offsetX = seed.x;
                offsetZ = seed.y;
            }

            meshBuilder.Clear();

            //make sure the player doesnt break things
            if (width <= 0)
            {
                width = 1;
                Debug.LogWarning("Width cannot be less than or equal to 0");
            }
            if (length <= 0)
            {
                length = 1;
                Debug.LogWarning("Length cannot be less than or equal to 0");
            }

            if (cubeScale <= 0)
            {
                Debug.LogWarning("CubeScale cannot be less than or equal to 0, defaulting to 1");
                cubeScale = 1.0f;
            }

            if (cubeDistance < 0)
            {
                Debug.LogWarning("CubeDistance cannot be less than 0, defaulting to 0");
                cubeDistance = 0.0f;
            }

            if (power <= 0)
            {
                Debug.LogWarning("Power cannot be less than or equal to 0, defaulting to 1");
                power = 1;
            }

            //for each cube to generate
            for (int x = 0; x < width; x++)
            {
                if ((x % 10) == 0 && loadingPanel)
                {
                    specifcLoadingText.text = "Generating cubes";
                    loadingBar.value = Mathf.Clamp01((float)x / (float)width);
                    loadingPercent.text = loadingBar.value * 100f + "%";
                    yield return null;
                }

                for (int z = 0; z < length; z++)
                {

                    //create variables for use in perlin noise
                    float xCoord = (float)x / width * scale + offsetX;
                    float zCoord = (float)z / length * scale + offsetZ;

                    //Generate the perlin noise
                    float perlin = Mathf.PerlinNoise(xCoord * refiner, zCoord * refiner);

                    //set the y pos to be perlin noise
                    float positionY = perlin;

                    //multiply the height for taller terrain
                    positionY *= heightMultiplier;

                    positionY = Mathf.Pow(positionY, power);

                    Vector3 pos = new Vector3(x * cubeScale + cubeDistance * x, roundToInt ? (int)positionY : positionY, z * cubeScale + cubeDistance * z);

                    meshBuilder.AddQuad(
                        Vector3.up,
                        pos,
                        Vector3.right * cubeScale,
                        Vector3.forward * cubeScale,
                        gradient.Evaluate(perlin)
                        );

                    SpawnRandomObjects(pos);
                    m_cubes[x, z] = pos;
                    perlinVals[x, z] = perlin;
                }
            }

            //for each face
            for (int x = 0; x < width; x++)
            {
                if ((x % 10) == 0 && loadingPanel)
                {
                    specifcLoadingText.text = "Generating faces";
                    loadingBar.value = Mathf.Clamp01((float)x / (float)width);
                    loadingPercent.text = loadingBar.value * 100f + "%";
                    yield return null;
                }

                for (int y = 0; y < length; y++)
                {
                    //if we can go one tile more in the z direction
                    if (y + 1 < length)
                    {
                        if (cubeDistance == 0)
                        {
                            if (Mathf.Abs(m_cubes[x, y].y - m_cubes[x, y + 1].y) > heightDifMin)
                            {
                                //if there is no distance between them we know the normal is going to be forward
                                Vector3 pos = new Vector3(m_cubes[x, y].x, m_cubes[x, y].y, m_cubes[x, y].z + cubeScale);
                                meshBuilder.AddQuad(
                                    Vector3.forward, //normal
                                    pos,  //position
                                    Vector3.right * cubeScale, //how far to place second point in x axis
                                    Vector3.up * (m_cubes[x, y + 1].y - m_cubes[x, y].y),//how far to place second point in y axis
                                    gradient.Evaluate(perlinVals[x, y])); //color
                            }
                        }
                        else
                        {
                            //when the walls will be at a slope

                            Vector3 pos = new Vector3(m_cubes[x, y].x, m_cubes[x, y].y, m_cubes[x, y].z + cubeScale);
                            meshBuilder.AddQuad(
                                Vector3.forward,
                                pos,
                                Vector3.right * cubeScale,
                                -Vector3.up * (m_cubes[x, y].y - m_cubes[x, y + 1].y) + (cubeDistance * Vector3.forward),
                                gradient.Evaluate(perlinVals[x, y]));
                        }


                    }

                    //do the same but for the x direction
                    if (x + 1 < width)
                    {
                        if (cubeDistance == 0)
                        {
                            if (Mathf.Abs(m_cubes[x, y].y - m_cubes[x + 1, y].y) > heightDifMin)
                            {
                                //if there is no distance between them we know the normal is going to be right

                                Vector3 pos = new Vector3(m_cubes[x, y].x + cubeScale, m_cubes[x, y].y, m_cubes[x, y].z + cubeScale);
                                meshBuilder.AddQuad(
                                    Vector3.right, // normal
                                    pos, // position
                                    -Vector3.forward * cubeScale, // x axis
                                    Vector3.up * (m_cubes[x + 1, y].y - m_cubes[x, y].y), // y axis
                                    gradient.Evaluate(perlinVals[x, y]));
                            }


                        }
                        else
                        {
                            //when the walls will be at a slope

                            Vector3 pos = new Vector3(m_cubes[x, y].x + cubeScale, m_cubes[x, y].y, m_cubes[x, y].z + cubeScale);
                            meshBuilder.AddQuad(
                                Vector3.right,
                                pos,
                                -Vector3.forward * cubeScale,
                                -Vector3.up * (m_cubes[x, y].y - m_cubes[x + 1, y].y) + (cubeDistance * Vector3.right),
                                gradient.Evaluate(perlinVals[x, y]));
                        }

                    }

                    //add corner planes if the distance is greater than 0 and we can place in corners
                    if (cubeDistance > 0 && x + 1 < width && y + 1 < length)
                    {
                        Vector3 point1 = new Vector3(m_cubes[x, y].x + cubeScale, m_cubes[x, y].y, m_cubes[x, y].z + cubeScale);

                        Vector3 point2 = new Vector3(m_cubes[x + 1, y].x, m_cubes[x + 1, y].y, m_cubes[x + 1, y].z + cubeScale);

                        Vector3 point3 = new Vector3(m_cubes[x, y + 1].x + cubeScale, m_cubes[x, y + 1].y, m_cubes[x, y + 1].z);

                        Vector3 point4 = new Vector3(m_cubes[x + 1, y + 1].x, m_cubes[x + 1, y + 1].y, m_cubes[x + 1, y + 1].z);

                        meshBuilder.AddQuad(
                            Vector3.up,
                            point1,
                            point2,
                            point3,
                            point4,
                            gradient.Evaluate(perlinVals[x, y]));
                    }
                }
            }

            if (loadingPanel)
            {
                meshBuilder.CreateMesh(allocateLargerMemory, loadingPanel, specifcLoadingText, loadingPercent, loadingBar);
            }
            else
            {
                meshBuilder.createMesh(allocateLargerMemory);
            }
        }

        /// <summary>
        /// Randomises where in the perlin noise seed the terrain will generate 
        /// </summary>
        public void RandomiseArea()
        {
            offsetX = Random.Range(0, 1000);
            offsetZ = Random.Range(0, 1000);
        }

        //A way to access the generate map function when in play mode
        [ContextMenu("UpdateTerrain")]
        void UpdateCubes()
        {
            GenerateCubes(new Vector2(offsetX, offsetZ));
        }

        //A way to access the generate map function when in play mode (generates a new seed)
        [ContextMenu("NewSeedTerrain")]
        void NewCubes()
        {
            GenerateCubes();
        }

        /// <summary>
        /// Randomises every variable in the terrain and generates a new terrain
        /// </summary>
        [ContextMenu("GenerateNewRandomTerrain")]
        public void GenerateRandomTerrain()
        {
            scale = Random.Range(0.1f, 50);

            power = Random.Range(0.3f, 3.0f);

            heightMultiplier = Random.Range(1, 10);

            int rand = Random.Range(0, 2);

            roundToInt = rand == 0 ? true : false;

            GenerateCubes();
        }

        /// <summary>
        /// Returns the passed in grid Position in worldSpace
        /// </summary>
        /// <param name="gridPos"></param>
        /// <returns></returns>
        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            Vector3 localPos = new Vector3();
            localPos.x = gridPos.x * (cubeScale + cubeDistance) + cubeScale * 0.5f;
            localPos.z = gridPos.y * (cubeScale + cubeDistance) + cubeScale * 0.5f;
            localPos.y = m_cubes[gridPos.x, gridPos.y].y;

            return transform.TransformPoint(localPos);
        }

        /// <summary>
        /// Returns the passed in worldPosition in grid Space
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            // transform into our space
            Vector3 localPos = transform.InverseTransformPoint(worldPos);

            Vector2Int gridPos = new Vector2Int();

            gridPos.x = Mathf.FloorToInt(localPos.x / (cubeScale + cubeDistance));
            gridPos.y = Mathf.FloorToInt(localPos.z / (cubeScale + cubeDistance));

            return gridPos;
        }

        public Vector3 SnapToGrid(Vector3 pos)
        {
            Vector2Int gridPos = WorldToGrid(pos);
            return GridToWorld(gridPos);
        }

        void SpawnRandomObjects(Vector3 position)
        {
            float randomSpawn = Random.Range(0, spawnChance);
            float runningSpawn = 0f;
            bool spawned = false;

            foreach (TerrainObject TO in terrainObjects)
            {
                if (!spawned)
                {
                    runningSpawn += TO.spawnChance;

                    if (randomSpawn < runningSpawn)
                    {
                        GameObject terrainObj = Instantiate(TO.model, transform);
                        terrainObj.transform.up = Vector3.up;
                        terrainObj.transform.localScale = new Vector3(TO.scale, TO.scale, TO.scale);
                        terrainObj.transform.position += new Vector3(position.x, position.y + TO.heightOffset, position.z);
                        spawned = true;
                    }
                }
            }
        }
    }
}
