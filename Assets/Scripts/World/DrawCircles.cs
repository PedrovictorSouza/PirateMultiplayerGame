using System.Collections.Generic;
using UnityEngine;

public class DrawCircles : MonoBehaviour, IIslandProvider
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private int numberOfIslands = 5;

    [SerializeField]
    private float minRadius = 100f, maxRadius = 500f;

    [SerializeField]
    private float islandSpawnRadius = 20f;

    private List<Coords> islands = new List<Coords>();

    void Start()
    {
        GenerateIslands(numberOfIslands);
    }

    void GenerateIslands(int count)
    {
        Vector3 playerPosition = player.position;
        Coords initialIsland = new Coords(playerPosition.x + Random.Range(-400, 400), playerPosition.y + Random.Range(-400, 400));
        islands.Add(initialIsland);

        float initialRadius = Random.Range(minRadius, maxRadius);
        Coords.DrawCircle(initialIsland, initialRadius, Color.yellow);

        for (int i = 1; i < count; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(5f, islandSpawnRadius);
            float offsetX = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
            float offsetY = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

            Coords island = new Coords(initialIsland.x + offsetX, initialIsland.y + offsetY);
            islands.Add(island);

            float randomRadius = Random.Range(minRadius, maxRadius);
            Coords.DrawCircle(island, randomRadius, Color.yellow);
        }
    }

    public Coords GetClosestIsland(Vector3 playerPosition)
    {
        Coords closestIsland = null;
        float closestDistance = float.MaxValue;

        foreach (Coords island in islands)
        {
            float distance = Vector3.Distance(playerPosition, island.ToVector3());
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIsland = island;
            }
        }

        return closestIsland;
    }
}
