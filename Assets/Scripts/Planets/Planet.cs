using UnityEngine;

namespace SpaceExploration.Planets
{
    public class Planet
    {
        private int _rating;

        public int Rating
        {
            get => _rating;
            set => _rating = Mathf.Clamp(value,1, 10000);
        }

        private Vector2 _coordinates;
        public Vector2 Coordinates
        {
            get => _coordinates;
            set => _coordinates = value;
        }
    }
}