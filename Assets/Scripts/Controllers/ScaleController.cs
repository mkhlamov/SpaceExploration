using System;
using SpaceExploration.Grid;
using UnityEngine;

namespace SpaceExploration.Controllers
{
    public class ScaleController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private GameManager gameManager;
        
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