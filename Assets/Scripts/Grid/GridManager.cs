﻿using System;
using System.Collections.Generic;
using System.Linq;
using SpaceExploration.Planets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceExploration.Grid
{
    [Serializable]
    public class GridManager
    {
        private int _minScale = 5;
        private int _maxScale = 100;

        private int _currentScale;
        public int CurrentScale => _currentScale;
        private Grid _currentGrid;
        public Grid CurrentGrid => _currentGrid;
        
        private GameObject _gridPrefab;
        private Transform _gridParent;

        private List<Grid> _grids = new List<Grid>();
        private List<GameObject> _tileGOs = new List<GameObject>();
        private Vector2 _prevCenterPosition;
        private Grid _renderGrid;
        public Grid RenderGrid => _renderGrid;

        public GridManager(GameObject gridPrefab, Transform gridParent, int minScale=5, int maxScale=100)
        {
            _gridPrefab = gridPrefab;
            _gridParent = gridParent;
            _minScale = minScale;
            _maxScale = maxScale;
            _currentScale = minScale;

            _currentGrid = GenerateNewGrid(Vector2.zero);
            _renderGrid = new Grid(_currentScale, _currentScale, Vector2.zero);
        }

        public Grid GetGridWithPositionInIt(Vector2 position)
        {
            try
            {
                return _grids.First(x => x.IsInGrid(position));
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        private void RenderCurrentScaleGrid(Vector2 centerPosition)
        {
            if (_tileGOs.Count == 0)
            {
                for (var i = 0; i < _currentScale; i++)
                {
                    for (var j = 0; j < _currentScale; j++)
                    {
                        var go = Object.Instantiate(_gridPrefab,
                            centerPosition + new Vector2(i - (_currentScale / 2), j - (_currentScale / 2)),
                            Quaternion.identity);
                        go.transform.parent = _gridParent;
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

        public void UpdateGrid(Vector2 centerPosition)
        {
            UpdateCurrentGrid(centerPosition);
            UpdateRenderGrid(centerPosition);
            RenderCurrentScaleGrid(centerPosition);
        }

        private void UpdateRenderGrid(Vector2 centerPosition)
        {
            _renderGrid = new Grid(_currentScale, _currentScale, centerPosition);
            
            if (GetGridWithPositionInIt(_renderGrid.TopLeftPosition) == null) GenerateGridForPosition(_renderGrid.TopLeftPosition);
            if (GetGridWithPositionInIt(_renderGrid.TopRightPosition) == null) GenerateGridForPosition(_renderGrid.TopRightPosition);
            if (GetGridWithPositionInIt(_renderGrid.BottomLeftPosition) == null) GenerateGridForPosition(_renderGrid.BottomLeftPosition);
            if (GetGridWithPositionInIt(_renderGrid.BottomRightPosition) == null) GenerateGridForPosition(_renderGrid.BottomRightPosition);
        }
        
        private void UpdateCurrentGrid(Vector2 centerPosition)
        {
            _currentGrid = GetGridWithPositionInIt(centerPosition);
        }

        public List<Planet> GetPlanetsInRenderGrid()
        {
            var grids = new List<Grid>();
            var g = GetGridWithPositionInIt(_renderGrid.TopLeftPosition);
            if (!grids.Contains(g)) grids.Add(g);
            
            g = GetGridWithPositionInIt(_renderGrid.TopRightPosition);
            if (!grids.Contains(g)) grids.Add(g);
            
            g = GetGridWithPositionInIt(_renderGrid.BottomLeftPosition);
            if (!grids.Contains(g)) grids.Add(g);
            
            g = GetGridWithPositionInIt(_renderGrid.BottomRightPosition);
            if (!grids.Contains(g)) grids.Add(g);

            var planets = new List<Planet>();
            foreach (var grid in grids)
            {
                planets.AddRange(grid.Planets.Where(x => _renderGrid.IsInGrid(x.Coordinates)));
            }
            return planets;
        }

        private void GenerateGridForPosition(Vector2 position)
        {
            if (_currentGrid.GridPosition.y + (_maxScale - 1) / 2 < position.y)
            {
                GenerateNewGrid(_currentGrid.GridPosition + Vector2.up * _maxScale);
            }

            if (_currentGrid.GridPosition.y - (_maxScale - 1) / 2 > position.y)
            {
                GenerateNewGrid(_currentGrid.GridPosition + Vector2.down * _maxScale);
            }
                
            if (_currentGrid.GridPosition.x + (_maxScale - 1) / 2 < position.x)
            {
                GenerateNewGrid(_currentGrid.GridPosition + Vector2.right * _maxScale);
            }
                
            if (_currentGrid.GridPosition.x - (_maxScale - 1) / 2 > position.x)
            {
                GenerateNewGrid(_currentGrid.GridPosition + Vector2.left * _maxScale);
            }
        }

        private Grid GenerateNewGrid(Vector2 gridPosition)
        {
            var grid = new Grid(_maxScale, _maxScale, gridPosition);
            grid.AddRandomPlanets();
            
            //transform.position = new Vector2(-grid.RowCount / 2, grid.ColCount / 2);
            _grids.Add(grid);
            return grid;
        }
    }
}
