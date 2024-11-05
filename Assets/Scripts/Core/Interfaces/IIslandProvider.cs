using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Core/ISpawnable.cs
public interface IIslandProvider
{
    Coords GetClosestIsland(Vector3 playerPosition);
}
