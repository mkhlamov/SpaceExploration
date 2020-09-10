using System;
using Input;
using UnityEngine;

namespace CompositionRoot
{
    public class App : MonoBehaviour
    {
        [SerializeField] private Player.Player player;

        private void Start()
        {
            var inputService = new KeyboardInput();

            player.InputService = inputService;
        }
    }
}