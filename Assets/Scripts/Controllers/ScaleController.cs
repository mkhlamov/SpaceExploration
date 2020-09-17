using System;
using SpaceExploration.Grid;
using SpaceExploration.Interfaces;
using UnityEngine;

namespace SpaceExploration.Controllers
{
    public class ScaleController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private GameManager gameManager;
        
        public IInputService InputService;

        
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
        }

        private void Update()
        {
            UpdateZoom();
        }

        private void UpdateZoom()
        {
            var zoom = InputService.GetZoomDelta();
            if (Mathf.Abs(zoom) > 0.1f)
            {
                var newScale = zoom > 0 ? -1 : 1;
                gameManager.ChangeScale(newScale);
            }
        }

        public void ScaleUp()
        {
            gameManager.ChangeScale(1);
        }
        
        public void ScaleDown()
        {
            gameManager.ChangeScale(-1);
        }
    }
}