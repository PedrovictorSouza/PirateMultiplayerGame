using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInputSystem
{
    private readonly Camera mainCamera;
    private readonly Ship shipReference;

    public ShipInputSystem(Camera camera, Ship ship)
    {
        mainCamera = camera;
        shipReference = ship;
    }

    public Vector3 GetWorldPositionFromMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}