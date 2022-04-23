using UnityEngine;

namespace ATH.GBS
{
    public class CoreGrid
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
        public Cell this[int x, int y] => GetCell(x, y);

        public CoreGrid(int width, int height, Vector3 rootPosition, Quaternion rootRotation, string gridId)
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
                    _grid[i, j] = new Cell();
                }
            }
        }

        public CoreGrid(bool[,] shape, Vector3 rootPosition, Quaternion rootRotation, string gridId)
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
                    _grid[i, j] = shape[i, j] ? new Cell() : null;
                }
            }
        }

        private Cell GetCell(int x, int y)
        {
            if (_width < x || _height < y || x < 0 || y < 0)
                return null;
            return _grid[x, y];
        }
       
    }
}
