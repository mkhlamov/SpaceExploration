﻿using System;
using SpaceExploration.Input;
using UnityEngine;

namespace SpaceExploration.CompositionRoot
{
    public class App : MonoBehaviour
    {
        [SerializeField] private Player.Player player;

        private void Awake()
        {
            //player.InputService = new KeyboardInput();
        }
    }
}