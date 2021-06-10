using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Zacks.Terrain
{
    /// <summary>
    /// Using the CubeGenerator, Generates a path using
    /// A* pathfinding.
    /// </summary>
    public class PathManager : MonoBehaviour
    {
        //[SerializeField]
        //bool debugMode = false;
        //GameObject[,] debugObj;


        [HideInInspector]
        public Tile[,] tiles;

        [HideInInspector]
        public CubeGenerator terrain;

        //set up the tiles
        public void SetTiles(CubeGenerator generator)
        {
            terrain = generator;

            //generate all the tiles
            tiles = new Tile[generator.width, generator.length];

            //if (debugMode)
            //    debugObj = new GameObject[generator.width, generator.length];

            for (int x = 0; x < generator.width; x++)
            {
                for (int z = 0; z < generator.length; z++)
                {
                    Tile newTile = new Tile();

                    newTile.gridPos.Set(x, z);
                    newTile.height = generator.Cubes[x, z].y;

                    tiles[x, z] = newTile;

                    //if (debugMode)
                    //{
                    //    debugObj[x, z] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    //    debugObj[x, z].transform.position = terrain.GridToWorld(newTile.gridPos);
                    //    debugObj[x, z].GetComponent<CapsuleCollider>().enabled = false;
                    //    debugObj[x, z].transform.localScale = new Vector3(.5f,.3f,.5f);
                    //    debugObj[x, z].transform.parent = this.transform;
                    //}

                }
            }

            //connect all the tiles
            for (int x = 0; x < generator.width; x++)
            {
                for (int z = 0; z < generator.length; z++)
                {
                    if (x + 1 < generator.width)
                    {
                        tiles[x, z].connections.Add(tiles[x + 1, z]);

                        tiles[x + 1, z].connections.Add(tiles[x, z]);
                    }
                    if (z + 1 < generator.length)
                    {
                        tiles[x, z].connections.Add(tiles[x, z + 1]);

                        tiles[x, z + 1].connections.Add(tiles[x, z]);
                    }
                }
            }
        }

        public List<Vector3> CalculatePath(Tile startTile, Tile endTile, float heightHateLevel)
        {

            List<Tile> openList = new List<Tile>();
            List<Tile> closedList = new List<Tile>();

            //if (startTile == null || endTile == null)
            //    return null;

            if (startTile == endTile)
                return new List<Vector3>();

            bool foundStart = false;
            bool foundEnd = false;

            //need to set startTile and endTile to the one with connections in the list
            foreach (Tile t in tiles)
            {
                if (t == startTile)
                {
                    startTile = t;
                    foundStart = true;

                    if (foundEnd)
                        break;

                }
                else if (t == endTile)
                {
                    endTile = t;
                    foundEnd = true;

                    if (foundStart)
                        break;
                }
            }

            if (!foundEnd || !foundStart)
            {
                Debug.LogError("Not found start or end");
                return new List<Vector3>();
            }


            startTile.Gcost = 0;

            openList.Add(startTile);

            Tile currentTile;

            //While there are still tiles to examine
            while (openList.Count > 0)
            {
                //sort the open list to check the lowest cost node
                openList = openList.OrderBy(x => x.Fcost).ToList();

                //set the current node to the closest one in open list
                currentTile = openList[0];

                //if its the end tile we found our path
                if (currentTile == endTile)
                {
                    endTile = currentTile;
                    break;
                }

                //remove it from open list so it isnt examined again
                openList.Remove(currentTile);
                closedList.Add(currentTile);


                //bool hasFoundEnd = false;
                //for all of the connections around it
                for (int i = 0; i < currentTile.connections.Count; i++)
                {

                    Tile connection = new Tile();
                    connection = currentTile.connections[i];

                    if (IsInList(connection, closedList)) { continue; }

                    //if(connection == endTile)
                    //{
                    //    endTile = connection;

                    //    hasFoundEnd = true;
                    //    connection.previousTile = currentTile;
                    //    break;
                    //}

                    //calculate costs
                    float Gscore = Mathf.Abs(connection.height - currentTile.height) * heightHateLevel;
                    float Hscore = Vector2Int.Distance(connection.gridPos, endTile.gridPos);
                    float Fscore = Gscore + Hscore;

                    if (!IsInList(connection, openList))
                    {
                        connection.Gcost = Gscore;
                        connection.Hcost = Hscore;
                        connection.Fcost = Fscore;

                        connection.previousTile = currentTile;

                        openList.Add(connection);
                    }
                    //if in the open list, check if the new path is better than old one
                    else if (Fscore < connection.Fcost)
                    {
                        connection.Gcost = Gscore;
                        connection.Hcost = Hscore;
                        connection.Fcost = Fscore;

                        connection.previousTile = currentTile;
                    }
                }
                //if (hasFoundEnd)
                //{
                //    break;
                //}
            }

            List<Vector3> path = new List<Vector3>();
            currentTile = endTile;

            //go back and add everything to the path

            int index = 0;

            while (currentTile.previousTile != null)
            {
                path.Add(terrain.GridToWorld(currentTile.gridPos));

                currentTile = currentTile.previousTile;

                index++;
                if (index > 100)
                {
                    Debug.LogError("SOMETHING GONE WRONG");
                    return new List<Vector3>();
                }
            }

            path.Reverse();


            //reset the lists
            foreach (Tile t in closedList)
            {
                t.previousTile = null;
            }
            foreach (Tile t in openList)
            {
                t.previousTile = null;
            }


            return path;
        }

        bool IsInList(Tile target, List<Tile> l)
        {
            foreach (Tile t in l)
            {
                if (t == target)
                    return true;
            }

            return false;
        }

        //public void UpdateDebugCubes()
        //{
        //    //if (!debugMode)
        //        return;

        //    //for (int x = 0; x < terrain.width; x++)
        //    //{
        //    //    for (int z = 0; z < terrain.length; z++)
        //    //    {
        //    //        switch (tiles[x, z].stoodOn)
        //    //        {
        //    //            case PlayerColor.Red:
        //    //                debugObj[x, z].GetComponent<MeshRenderer>().material.color = Color.red;
        //    //                break;
        //    //            case PlayerColor.Blue:
        //    //                debugObj[x, z].GetComponent<MeshRenderer>().material.color = Color.blue;
        //    //                break;
        //    //            case PlayerColor.Green:
        //    //                debugObj[x, z].GetComponent<MeshRenderer>().material.color = Color.green;
        //    //                break;
        //    //            case PlayerColor.Yellow:
        //    //                debugObj[x, z].GetComponent<MeshRenderer>().material.color = Color.yellow;
        //    //                break;
        //    //            default:
        //    //                debugObj[x, z].GetComponent<MeshRenderer>().material.color = Color.white;
        //    //                break;
        //    //        }
        //    //
        //    //    }
        //    //}
        //}
    }
}
