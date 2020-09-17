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
#if UNITY_STANDALONE || UNITY_EDITOR
            player.InputService = new KeyboardInput();
#elif  UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            player.InputService = new MobileInput();
#endif

            var gridManager = new GridManager(gridTilePrefab, gridParent, minScale: 5, maxScale: 11);
            gameManager.SetGridManager(gridManager);
        }
    }
}