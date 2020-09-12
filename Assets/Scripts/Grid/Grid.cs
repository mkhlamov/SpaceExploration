using UnityEngine;

namespace SpaceExploration.Grid
{
    public class Grid
    {
        private int _rowCount;
        public int RowCount => _rowCount;
        
        private int _colCount;
        public int ColCount => _colCount;

        private Vector2 _gridPos;
        public Vector2 GridPosition => _gridPos;

        public Grid(int rows, int cols, Vector2 gridPos)
        {
            _rowCount = rows;
            _colCount = cols;
            _gridPos = gridPos;
        }

        public bool IsInGrid(Vector2 position)
        {
            return (position.x > _gridPos.x - (_colCount - 1) / 2) &&
                   (position.x < _gridPos.x + (_colCount - 1) / 2) &&
                   (position.y > _gridPos.y - (_rowCount - 1) / 2) &&
                   (position.y < _gridPos.y + (_rowCount - 1) / 2);
        }

        public bool IsOnBorder(Vector2 position)
        {
            return (position.x == _gridPos.x - (_colCount - 1) / 2) ||
                   (position.x == _gridPos.x + (_colCount - 1) / 2) ||
                   (position.y == _gridPos.y - (_rowCount - 1) / 2) ||
                   (position.y == _gridPos.y + (_rowCount - 1) / 2);
        }
    }
}