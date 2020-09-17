using System;
using SpaceExploration.Interfaces;
using UnityEngine;

namespace SpaceExploration.Input
{
    public class MobileInput : IInputService
    {
        private Vector2 _touchOrigin = -Vector2.one;
        
        private Vector2 _touchZoomZeroOrigin = -Vector2.one;
        private Vector2 _touchZoomOneOrigin = -Vector2.one;

        public Vector2 GetMovementInputDelta()
        {
            var movement = Vector2.zero;
            if (UnityEngine.Input.touchCount == 1)
            {
                var touch = UnityEngine.Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchOrigin = touch.position;
                        break;
                    case TouchPhase.Ended when _touchOrigin.x >= 0:
                    {
                        var touchEnd = touch.position;
                        var x = (touchEnd.x - _touchOrigin.x) / Screen.width;
                        var y = (touchEnd.y - _touchOrigin.y) / Screen.height;
                        movement = new Vector2(Mathf.Abs(x) > 0.1f ? x : 0f,
                            Mathf.Abs(y) > 0.1f ? y : 0f);
                        _touchOrigin.x = -1;
                        break;
                    }
                }
            }

            return movement;
        }

        public float GetZoomDelta()
        {
            var zoomDelta = 0f;
            if (UnityEngine.Input.touchCount == 2)
            {
                var touchZero = UnityEngine.Input.GetTouch(0);
                var touchOne = UnityEngine.Input.GetTouch(1);

                switch (touchZero.phase)
                {
                    case TouchPhase.Began:
                        _touchZoomZeroOrigin = touchZero.position;
                        _touchZoomOneOrigin = touchOne.position;
                        break;
                    case TouchPhase.Ended:
                        var prevMagnitude = (_touchZoomZeroOrigin - _touchZoomOneOrigin).magnitude;
                        var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                        zoomDelta = currentMagnitude - prevMagnitude;
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return zoomDelta / 100f;
        }
    }
}