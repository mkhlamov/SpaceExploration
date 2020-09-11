using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceExploration.Grid
{
    public class GridManager
    {
        private int _minScale = 5;
        private int _maxScale = 100;

        private int _currentScale;
        public int CurrentScale => _currentScale;
        private int _currentGrid;

        private List<Grid> _grids = new List<SpaceExploration.Grid.Grid>();

        public GridManager(int minScale=5, int maxScale=100)
        {
            _minScale = minScale;
            _maxScale = maxScale;
            _currentScale = minScale;
            GenerateNewGrid(Vector2.zero);
            _currentGrid = _grids.Count - 1;
        }

        public Grid GetCurrentGrid()
        {
            return _grids[_currentGrid];
        }

        public Grid GetGridWithPositionInIt(Vector2 position)
        {
            Debug.Log(position);
            Debug.Log(-_grids.IndexOf(_grids.First(x => x.IsInGrid(position))));
            
            return _grids.First(x => x.IsInGrid(position));
        }
        
        private void GenerateNewGrid(Vector2 gridPosition)
        {
            var grid = new Grid(_maxScale, _maxScale, gridPosition);
            
            //transform.position = new Vector2(-grid.RowCount / 2, grid.ColCount / 2);
            _grids.Add(grid);
        }
    }
}
