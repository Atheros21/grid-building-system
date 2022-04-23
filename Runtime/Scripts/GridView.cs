using System.Collections.Generic;
using UnityEngine;

namespace ATH.GBS
{
    public class GridView : MonoBehaviour, IGridView
    {
        [SerializeField] private CellView _cellView;

        private CoreGrid _grid;
        private Dictionary<Vector2Int, ICellView> _coordinateToCellView;
        private Dictionary<ICellView, Vector2Int> _cellViewToCoordinate;

        public ICellView CellView => _cellView;

        public void ChangeCellsState(List<Vector2Int> coordinates, CellViewState state)
        {
            foreach (var pos in coordinates)
            {
                if (!_coordinateToCellView.ContainsKey(pos)) continue;

                _coordinateToCellView[pos].SetState(state);
            }
        }

        public Vector2Int? GetCellCoordinate(ICellView cellView)
        {
            if (!_cellViewToCoordinate.ContainsKey(cellView)) return null;

            return _cellViewToCoordinate[cellView];
        }

        public void Render(CoreGrid grid)
        {
            _coordinateToCellView = new Dictionary<Vector2Int, ICellView>();
            _cellViewToCoordinate = new Dictionary<ICellView, Vector2Int>();

            var rootObject = new GameObject(grid.GridId);
            rootObject.transform.SetParent(transform);
            rootObject.transform.position = grid.Position;
            rootObject.transform.rotation = grid.Roation;

            for (var x = 0;  x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    if (grid[x, y] == null) continue;

                    var newView = CellView.Create(CellViewState.Normal, grid.Position+new Vector3(x,0,y),grid.Roation, rootObject.transform);
                    _coordinateToCellView.Add(new Vector2Int(x, y), newView);
                    _cellViewToCoordinate.Add(newView, new Vector2Int(x, y));
                }
            }
        }
    }
}