using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    public class GridView : MonoBehaviour, IGridView
    {
        public GameObject floorPrefab;

        private CoreGrid _grid;

        private void Start()
        {
            _grid = GridUtilityTest.GetHGridShaped();
            Render(_grid);
        }

        public void Highlight(List<Vector2Int> coordinates, CoreGrid grid, object style)
        {
            throw new System.NotImplementedException();
        }

        public void Render(CoreGrid grid)
        {
            GameObject rootObject = new GameObject(grid.GridId);
            rootObject.transform.position = grid.Position;
            rootObject.transform.rotation = grid.Roation;
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    if (grid[i, j] != null)
                    {
                        Vector3 instatiationPosition = rootObject.transform.position + rootObject.transform.forward * i + rootObject.transform.right * j;
                        Instantiate(floorPrefab, instatiationPosition, rootObject.transform.rotation, rootObject.transform);
                    }
                }
            }
        }
    }
}