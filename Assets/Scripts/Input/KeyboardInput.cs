using SpaceExploration.Interfaces;
using UnityEngine;

namespace SpaceExploration.Input
{
    public class KeyboardInput : IInputService
    {
        private Vector2 _movementInput;

        private bool _movementButtonDown = false;
        
        public Vector2 GetMovementInputDelta()
        {
            if (UnityEngine.Input.GetButtonDown("Horizontal") || UnityEngine.Input.GetButtonDown("Vertical"))
            {
                _movementButtonDown = true;
            }

            if (_movementButtonDown)
            {
                _movementInput = new Vector2(UnityEngine.Input.GetAxis("Horizontal"),
                    UnityEngine.Input.GetAxis("Vertical"));
            }

            if ((UnityEngine.Input.GetButtonUp("Horizontal") || UnityEngine.Input.GetButtonUp("Vertical")) && _movementButtonDown)
            {
                _movementButtonDown = false;
                return _movementInput;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }
}