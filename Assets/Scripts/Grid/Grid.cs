using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grid
{
    public class Grid
    {
        private int _rowCount;
        public int RowCount => _rowCount;
        
        private int _colCount;
        public int ColCount => _colCount;

        private Vector2 _gridPos;

        public Grid(int rows, int cols, Vector2 gridPos)
        {
            _rowCount = rows;
            _colCount = cols;
            _gridPos = gridPos;
        }
    }
}