using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PieceSpawningSystem;

namespace Zacks.Terrain
{
    /// <summary>
    /// Used in the PathManager
    /// </summary>
    public class Tile
    {
        public static bool operator ==(Tile c1, Tile c2)
        {
            if (Equals(null, c1) || Equals(null, c2))
            {
                if (Equals(null, c1) && Equals(null, c2))
                    return true;
                return false;
            }

            return c1.gridPos == c2.gridPos;
        }

        public static bool operator !=(Tile c1, Tile c2)
        {
            if (Equals(null, c1) || Equals(null, c2))
            {
                if (Equals(null, c1) && Equals(null, c2))
                    return false;
                return true;
            }



            return !(c1.gridPos == c2.gridPos);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Vector2Int gridPos = new Vector2Int();
        public float height = 0;

        //overall score to determine when to be calculated
        public float Fcost = 0;

        //difference in height
        public float Gcost = 0;

        //distance from endnode
        public float Hcost = 0;

        public List<Tile> connections = new List<Tile>();
        public Tile previousTile;


        ////remove or edit these to suit your personal needs////
        //public PlayerColor stoodOn = PlayerColor.none;      //
        //public PieceMovement stoodOnObject = null;          //
        //                                                    //remove or edit these to suit your personal needs////
    }
}
