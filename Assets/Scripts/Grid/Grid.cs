using System;
using System.Collections.Generic;
using System.Linq;
using SpaceExploration.Planets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceExploration.Grid
{
    [Serializable]
    public class Grid
    {
        private int _rowCount;
        public int RowCount => _rowCount;
        
        private int _colCount;
        public int ColCount => _colCount;
        public Vector2 TopLeftPosition => _gridPos + new Vector2(- (_colCount - 1) / 2,(_rowCount - 1) / 2);
        public Vector2 TopRightPosition => _gridPos + new Vector2((_colCount - 1) / 2,(_rowCount - 1) / 2);
        public Vector2 BottomLeftPosition => _gridPos + new Vector2(- (_colCount - 1) / 2,- (_rowCount - 1) / 2);
        public Vector2 BottomRightPosition => _gridPos + new Vector2((_colCount - 1) / 2,- (_rowCount - 1) / 2);

        private Vector2 _gridPos;

        public Vector2 GridPosition => _gridPos;
        private Dictionary<Vector2, Planet> _planetPositions = new Dictionary<Vector2, Planet>();

        public List<Planet> Planets => _planetPositions.Values.ToList();

        public Grid(int rows, int cols, Vector2 gridPos)
        {
            _rowCount = rows;
            _colCount = cols;
            _gridPos = gridPos;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            var g = (Grid) obj;
            return (_gridPos == g._gridPos) && (_rowCount == g._rowCount) && (_colCount == g._colCount);
        }

        public override string ToString()
        {
            return $"{TopLeftPosition} {TopRightPosition} {BottomLeftPosition} {BottomRightPosition} center {_gridPos}";
        }

        public bool IsInGrid(Vector2 position)
        {
            return (position.x >= _gridPos.x - (_colCount - 1) / 2) &&
                   (position.x <= _gridPos.x + (_colCount - 1) / 2) &&
                   (position.y >= _gridPos.y - (_rowCount - 1) / 2) &&
                   (position.y <= _gridPos.y + (_rowCount - 1) / 2);
        }

        public bool IsOnBorder(Vector2 position)
        {
            return (position.x == _gridPos.x - (_colCount - 1) / 2) ||
                   (position.x == _gridPos.x + (_colCount - 1) / 2) ||
                   (position.y == _gridPos.y - (_rowCount - 1) / 2) ||
                   (position.y == _gridPos.y + (_rowCount - 1) / 2);
        }

        public void AddRandomPlanets(float planetFillRate=0.3f)
        {
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColCount; j++)
                {
                    // randomly decide if we need to put a planet
                    if (!(Random.Range(0f, 1f) <= planetFillRate)) continue;
                    var planetPos = new Vector2(-(_colCount - 1) / 2 + i, 
                        -(_rowCount - 1) / 2 + j) + GridPosition;
                    var planet = new Planet(planetPos, 
                        Random.Range(1, 10001));
                    
                    _planetPositions[planetPos] = planet;
                }
            }
        }
    }
}