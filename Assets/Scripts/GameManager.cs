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
        [SerializeField] private Camera camera;
        [SerializeField] private GameObject gridTilePrefab;
        [SerializeField] private Transform gridParent;
        
        private GridManager _gridManager;
        private Vector2 _centerPosition;
        private List<Planet> _planets = new List<Planet>();
        private Dictionary<Planet, GameObject> _planetGOs = new Dictionary<Planet, GameObject>();

        #region Monobehaviour methods
        
        private void Start()
        {
            if (camera == null)
            {
                if (Camera.main != null)
                {
                    camera = Camera.main;
                }
                else
                {
                    Debug.LogError("No camera found!");
                }
            }
            _centerPosition = player.transform.position;
            //_gridManager = new GridManager(gridTilePrefab, gridParent, minScale:11, maxScale:11);
            
            Render();
        }

        private void OnEnable()
        {
            player.OnPlayerMoved += PlayerMoved;
        }
        
        private void OnDisable()
        {
            player.OnPlayerMoved -= PlayerMoved;
        }
        
        #endregion

        public void SetGridManager(GridManager gridManager)
        {
            if (gridManager != null)
            {
                _gridManager = gridManager;
            }
            else
            {
                throw new ArgumentNullException(nameof(gridManager));
            }
        }

        public void ChangeScale(int diff)
        {
            _gridManager.CurrentScale += diff * 2;
            Render();
        }
        
        private void FillCurrentGrid()
        {
            var currentGrid = _gridManager.CurrentGrid;
            for (var i = 0; i < _gridManager.CurrentScale; i++)
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
            /// ?????
            camera.transform.position = new Vector3(_centerPosition.x, _centerPosition.y, camera.transform.position.z);
            
            _gridManager.UpdateGrid(_centerPosition);
            RenderPlanets();
            SetCameraSize();
        }

        private void RenderPlanets()
        {
            var planetsInGrid = _gridManager.GetPlanetsInRenderGrid();
            planetsInGrid.Sort(PlanetComparer);

            // use pool
            if (_planetGOs.Count > 50) _planetGOs.Clear();

            foreach (var p in planetsInGrid)
            {
                if (!_planetGOs.TryGetValue(p, out var pgo))
                {
                    pgo = Instantiate(planetPrefab, p.Coordinates, Quaternion.identity);
                    pgo.GetComponent<PlanetObject>().Init(planetSprites[Random.Range(0, planetSprites.Count)], p.Rating,
                        planetsInGrid.IndexOf(p) <= 10);
                    pgo.transform.parent = gridParent;
                    _planetGOs[p] = pgo;
                }
                else
                {
                    pgo.SetActive(true);
                    _planetGOs[p].GetComponent<PlanetObject>().ShowText(planetsInGrid.IndexOf(p) <= 10);
                }
            }

            foreach (var pgoPair in _planetGOs)
            {
                pgoPair.Value.SetActive(planetsInGrid.Contains(pgoPair.Key));
            }
        }


        private int PlanetComparer(Planet x, Planet y)
        {
            var playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            var dist_x = (x.Coordinates - playerPosition).sqrMagnitude;
            var dist_y = (y.Coordinates - playerPosition).sqrMagnitude;

            return dist_x.CompareTo(dist_y);
        }
        
        private void PlayerMoved(Vector2 playerPosition)
        {
            if (_gridManager.RenderGrid.IsOnBorder(playerPosition))
            {
                _centerPosition = playerPosition;
                Render();
            }
        }

        private void SetCameraSize()
        {
            camera.orthographicSize = (_gridManager.CurrentScale - 0.5f) / 1.5f;
        }
    }
}