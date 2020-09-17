using SpaceExploration.Interfaces;
using UnityEngine;

namespace SpaceExploration.Input
{
    public class MobileInput : IInputService
    {
        private Vector2 touchOrigin = -Vector2.one;
        public Vector2 GetMovementInputDelta()
        {
            var movement = Vector2.zero;
            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchOrigin = touch.position;
                        break;
                    case TouchPhase.Ended when touchOrigin.x >= 0:
                    {
                        var touchEnd = touch.position;
                        var x = (touchEnd.x - touchOrigin.x) / Screen.width;
                        var y = (touchEnd.y - touchOrigin.y) / Screen.height;
                        movement = new Vector2(Mathf.Abs(x) > 0.1f ? x : 0f,
                            Mathf.Abs(y) > 0.1f ? y : 0f);
                        touchOrigin.x = -1;
                        break;
                    }
                }
            }

            return movement;
        }

        public float GetZoomDelta()
        {
            return 0;
        }
    }
}