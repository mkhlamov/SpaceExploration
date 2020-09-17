using UnityEngine;

namespace SpaceExploration.Interfaces
{
    public interface IInputService
    {
        Vector2 GetMovementInputDelta();
        float GetZoomDelta();
    }
}