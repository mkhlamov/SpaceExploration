using System.Collections.Generic;
using UnityEngine;

namespace SpaceExploration.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int maxScale = 100;
        [SerializeField] private int minScale = 5;

        private int _currentScale;
        private int _currentGrid;

        private List<SpaceExploration.Grid.Grid> _grids = new List<SpaceExploration.Grid.Grid>();

        private void Start()
        {
            _currentScale = minScale;
            GenerateNewGrid(Vector2.zero);
            _currentGrid = _grids.Count - 1;
        }
        
        private void GenerateNewGrid(Vector2 gridPosition)
        {
            var grid = new SpaceExploration.Grid.Grid(maxScale, maxScale, gridPosition);
            
            transform.position = new Vector2(-grid.RowCount / 2, grid.ColCount / 2);
            _grids.Add(grid);
        }
    }
}
