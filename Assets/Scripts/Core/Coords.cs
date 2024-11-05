using System.Collections.Generic;
using UnityEngine;

public class Coords
{
    public float x;
    public float y;
    public float z;

    public Coords(float _X, float _Y)
    {
        x = _X;
        y = _Y;
        z = -1;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return $"({x}, {y}, {z})";
    }

    // Desenha o círculo representando a ilha
    public static void DrawCircle(Coords center, float radius, Color color)
    {
        int segments = 100;
        GameObject circle = new GameObject("Circle_" + center.ToString());
        LineRenderer lineRenderer = circle.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.widthMultiplier = 1f; // Aumenta a espessura do círculo
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * (2f * Mathf.PI / segments);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(center.x + x, center.y + y, center.z));
        }
    }
}
