using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementSystem
{
    private readonly Transform transform;
    private readonly float speed;
    private Vector3 targetPosition;
    
    public bool IsMoving { get; private set; }

    public ShipMovementSystem(Transform shipTransform, float moveSpeed)
    {
        transform = shipTransform;
        speed = moveSpeed;
        targetPosition = transform.position;
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        IsMoving = true;
    }

    public void UpdateMovement()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            IsMoving = false;
        }
    }
}