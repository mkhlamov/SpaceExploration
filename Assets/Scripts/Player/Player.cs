using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public IInputService InputService;

        [SerializeField] private float sensitivity;

        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = new PlayerMovement(sensitivity);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = _playerMovement.GetNewPosition(transform.position, InputService.GetMovementInputDelta());
        }
    }
}