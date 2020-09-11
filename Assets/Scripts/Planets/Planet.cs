using UnityEngine;

namespace SpaceExploration.Planets
{
    public class Planet
    {
        private Vector2 _coordinates;
        public Vector2 Coordinates => _coordinates;

        private int _rating;

        public int Rating => _rating;

        public Planet(Vector2 coordinates, int rating)
        {
            _coordinates = coordinates;
            _rating = Mathf.Clamp(rating, 1, 10000);
        }
    }
}