using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private readonly float _sensitivity;

        public PlayerMovement(float sensitivity)
        {
            _sensitivity = sensitivity;
        }

        public Vector2 GetNewPosition(Vector2 startPos, Vector2 delta)
        {
            int GetTiles(float val)
            {
                if (Mathf.Abs(val) < _sensitivity) return 0;
                return val == 0 ? 0 : (int)Mathf.Sign(val);
            }
            return startPos + new Vector2(GetTiles(delta.x), GetTiles(delta.y));
        }
    }
}