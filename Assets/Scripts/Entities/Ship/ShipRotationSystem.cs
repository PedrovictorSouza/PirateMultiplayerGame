using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotationSystem
{
    private readonly Transform transform;

    public ShipRotationSystem(Transform shipTransform)
    {
        transform = shipTransform;
    }

    public void RotateTo(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}