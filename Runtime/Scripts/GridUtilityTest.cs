using UnityEngine;

namespace ATH.HouseBuilding
{
    public class GridUtilityTest
    {
        public static CoreGrid GetHGridShaped()
        {

            bool[,] gridShape = new bool[10, 10]
            {
                {true,true,true,false,false,false,false,true,true,true },
                {true,true,true,false,false,false,false,true,true,true },

                {true,true,true,false,false,false,false,true,true,true },
                {true,true,true,true,true,true,true,true,true,true },
                {true,true,true,true,true,true,true,true,true,true },
                {true,true,true,true,true,true,true,true,true,true },
                {true,true,true,true,true,true,true,true,true,true },
                {true,true,true,false,false,false,false,true,true,true },

                {true,true,true,false,false,false,false,true,true,true },
                {true,true,true,false,false,false,false,true,true,true }
            };

            CoreGrid coreGrid = new CoreGrid(gridShape, Vector3.zero, Quaternion.identity, "H");

            return coreGrid;
        }
    }
}