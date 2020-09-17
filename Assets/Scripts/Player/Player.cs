using System;
using Player;
using SpaceExploration.Input;
using SpaceExploration.Interfaces;
using UnityEngine;

namespace SpaceExploration.Player
{
    public class Player : MonoBehaviour
    {
        public IInputService InputService;
        public Action<Vector2> OnPlayerMoved;

        [SerializeField] private float sensitivity;

        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = new PlayerMovement(sensitivity);
            //InputService = new KeyboardInput();
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var newPos = _playerMovement.GetNewPosition(transform.position, 
                InputService.GetMovementInputDelta());
            
            if (newPos != new Vector2(transform.position.x, transform.position.y))
            {
                SetPosition(newPos);
                OnPlayerMoved?.Invoke(newPos);
            }
        }
    }
}