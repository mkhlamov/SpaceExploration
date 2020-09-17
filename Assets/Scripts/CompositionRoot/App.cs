using System;
using SpaceExploration.Controllers;
using SpaceExploration.Grid;
using SpaceExploration.Input;
using UnityEngine;

namespace SpaceExploration.CompositionRoot
{
    public class App : MonoBehaviour
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private GameManager gameManager;
        
        [SerializeField] private GameObject gridTilePrefab;
        [SerializeField] private Transform gridParent;

        private void Awake()
        {
            player.InputService = new KeyboardInput();

            var gridManager = new GridManager(gridTilePrefab, gridParent, minScale: 5, maxScale: 11);
            gameManager.SetGridManager(gridManager);
        }
    }
}