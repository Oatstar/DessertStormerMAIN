using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.AI.Navigation;
//using UnityEngine.AI;
//using Unity.AI;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;


public class ObstacleManager : MonoBehaviour
{
    List<GameObject> allObstacles = new List<GameObject>();
    [SerializeField] GameObject obstacleContainer;

    [SerializeField] GameObject[] obstaclePrefabs;

    Vector2 minSpawnPoint = new Vector2(-8f, -6f);
    Vector2 maxSpawnPoint = new Vector2(8f, 3f);

    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

    public GameObject navMeshObject;
    public NavMeshSurface navMeshSurface;
   
    public static ObstacleManager instance;

    private void Awake()
    {
        instance = this;

        navMeshSurface = navMeshObject.GetComponent<NavMeshSurface>();
    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnObstacle();
        }

        navMeshSurface.BuildNavMeshAsync();
    }

    public void SpawnObstacle()
    {
        Vector2 spawnPoint = GetRandomAvailableSpawnPoint();

        GameObject spawnedObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPoint, Quaternion.identity);
        spawnedObstacle.transform.SetParent(obstacleContainer.transform);
        allObstacles.Add(spawnedObstacle);
        occupiedPositions.Add(spawnPoint);
    }

    Vector2 GetRandomAvailableSpawnPoint()
    {
        Vector2 spawnPoint;
        int maxAttempts = 100; // Avoid infinite loop
        int attempts = 0;

        do
        {
            spawnPoint = GetRandomSpawnPoint();
            attempts++;
        } while ((occupiedPositions.Contains(spawnPoint) || EnemySpawner.instance.IsPositionOccupied(spawnPoint)) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Could not find an available spawn point after 100 attempts");
        }

        Debug.Log("attempt: " + attempts);
        return spawnPoint;
    }

    Vector2 GetRandomSpawnPoint()
    {
        int randomX = Mathf.RoundToInt(Random.Range(minSpawnPoint.x, maxSpawnPoint.x));
        int randomY = Mathf.RoundToInt(Random.Range(minSpawnPoint.y, maxSpawnPoint.y));
        Debug.Log("randomX: " + randomX+ " ... RandomY: "+randomY);
        return new Vector2(randomX, randomY);
    }

    public void ClearObstacles()
    {
        foreach (GameObject obstacle in allObstacles)
        {
            Destroy(obstacle);
        }
        occupiedPositions.Clear();
        allObstacles.Clear();
    }

    public void ObstacleRemoved(GameObject removedObstacle)
    {
        if (removedObstacle.GetComponent<ObstacleController>().GetObstacleType() != "destroyable")
            return;

        Vector2 obstaclePosition = removedObstacle.transform.position;
        allObstacles.Remove(removedObstacle);
        occupiedPositions.Remove(obstaclePosition);
        Destroy(removedObstacle);
    }
}