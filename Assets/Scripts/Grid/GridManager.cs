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
        private GameObject _gridPrefab;

        private List<Grid> _grids = new List<Grid>();
        private List<GameObject> _tileGOs = new List<GameObject>();
        private Vector2 _prevCenterPosition;

        public GridManager(GameObject gridPrefab, int minScale=5, int maxScale=100)
        {
            _gridPrefab = gridPrefab;
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

        public void RenderCurrentScaleGrid(Vector2 centerPosition)
        {
            if (_tileGOs.Count == 0)
            {
                for (var i = 0; i < _currentScale; i++)
                {
                    for (var j = 0; j < _currentScale; j++)
                    {
                        var go = GameObject.Instantiate(_gridPrefab,
                            centerPosition + new Vector2(i - (_currentScale / 2), j - (_currentScale / 2)),
                            Quaternion.identity);
                        _tileGOs.Add(go);
                    }
                }
            }
            else
            {
                foreach (var tile in _tileGOs)
                {
                    var diff = centerPosition - _prevCenterPosition;
                    tile.transform.position = tile.transform.position + new Vector3(diff.x,
                        diff.y, tile.transform.position.z);
                }
            }
            
            _prevCenterPosition = centerPosition;
        }

        private void GenerateNewGrid(Vector2 gridPosition)
        {
            var grid = new Grid(_maxScale, _maxScale, gridPosition);
            
            //transform.position = new Vector2(-grid.RowCount / 2, grid.ColCount / 2);
            _grids.Add(grid);
        }
    }
}
