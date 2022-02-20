using UnityEngine;

namespace ATH.HouseBuilding
{
    public class BuildingGrid
    {
        private string _gridId;
        private int _width, _height;
        private Cell[,] _grid;
        private Vector3 _position;
        private Quaternion _roation;

        public string GridId => _gridId;
        public int Width => _width;
        public int Height => _height;
        public Vector3 Position => _position;
        public Quaternion Roation => _roation;

        public Cell this[Vector2Int pos] => GetCell(pos.x, pos.y);

        public BuildingGrid(int width, int height, Vector3 rootPosition, Quaternion rootRotation, string gridId)
        {
            _width = width;
            _height = height;
            _grid = new Cell[_width, _height];
            _position = rootPosition;
            _gridId = gridId;
            _roation = rootRotation;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _grid[i, j] = new Cell(i, j);
                }
            }
        }

        public BuildingGrid(bool[,] shape, Vector3 rootPosition, Quaternion rootRotation, string gridId)
        {
            _width = shape.GetLength(0);
            _height = shape.GetLength(1);
            _grid = new Cell[_width, _height];
            _position = rootPosition;
            _gridId = gridId;
            _roation = rootRotation;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _grid[i, j] = shape[i, j] ? new Cell(i, j) : null;
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            if (_width < x || _height < y || x < 0 || y < 0)
                return null;
            return _grid[x, y];
        }

        public bool CanConstruct(Attachment attachment, int x, int y)
        {
            for (var i = 0; i < attachment.Shape.GetLength(0); i++)
            {
                for (var j = 0; j < attachment.Shape.GetLength(1); j++)
                {
                    if (!attachment.Shape[i, j]) continue;

                    var shapePointCoord = new Vector2Int(i - attachment.ShapeOrigin.x, j - attachment.ShapeOrigin.y);
                    var positionOnGrid = shapePointCoord + new Vector2Int(x, y);
                    var cell = this[positionOnGrid];

                    if (cell == null) return false;

                    if (cell.ContainsOcupiedSlot(attachment.OcupiedSlot)) return false;

                    if (!cell.ContainsEnablesSlot(attachment.OcupiedSlot)) return false;

                }

            }

            return true;
        }
    }
}
