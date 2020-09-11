using System;
using System.Collections.Generic;
using System.Linq;
using SpaceExploration.Grid;
using SpaceExploration.Planets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceExploration
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float planetFillRate = 0.3f;
        [SerializeField] private Player.Player player;
        [SerializeField] private GameObject planetPrefab;
        [SerializeField] private List<Sprite> planetSprites;
        
        private GridManager _gridManager;
        private Vector2 _centerPosition;
        private List<Planet> _planets = new List<Planet>();
        private Dictionary<Planet, GameObject> _planetGOs = new Dictionary<Planet, GameObject>();

        private void Start()
        {
            _gridManager = new GridManager();
            _centerPosition = player.transform.position;

            FillCurrentGrid();
            Render();
        }

        private void FillCurrentGrid()
        {
            var currentGrid = _gridManager.GetCurrentGrid();
            for (var i = 0; i < currentGrid.RowCount; i++)
            {
                for (var j = 0; j < currentGrid.ColCount; j++)
                {
                    // randomly decide if we need to put a planet
                    if (!(Random.Range(0f, 1f) <= planetFillRate)) continue;
                    var planet = new Planet(new Vector2(i, j) + currentGrid.GridPosition, 
                        Random.Range(1, 10001));
                    _planets.Add(planet);
                }
            }
        }

        private void Render()
        {
            var playerPosition = player.transform.position;
            var renderGrid = new Grid.Grid(_gridManager.CurrentScale, _gridManager.CurrentScale, _centerPosition);
            var planetsInGrid = _planets.Where(x => renderGrid.IsInGrid(x.Coordinates)).ToList();
            planetsInGrid.Sort(PlanetComparer);

            foreach (var p in planetsInGrid)
            {
                if (!_planetGOs.TryGetValue(p, out var pgo))
                {
                    pgo = Instantiate(planetPrefab, p.Coordinates, Quaternion.identity);
                    pgo.GetComponent<PlanetObject>().Init(planetSprites[Random.Range(0, planetSprites.Count)], p.Rating, planetsInGrid.IndexOf(p) <= 10);
                    _planetGOs[p] = pgo;
                }
                else
                {
                    _planetGOs[p].GetComponent<PlanetObject>().ShowText(planetsInGrid.IndexOf(p) <= 10);
                }
            }
        }

        private int PlanetComparer(Planet x, Planet y)
        {
            var playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            var dist_x = (x.Coordinates - playerPosition).sqrMagnitude;
            var dist_y = (y.Coordinates - playerPosition).sqrMagnitude;

            return dist_x.CompareTo(dist_y);
        }
    }
}