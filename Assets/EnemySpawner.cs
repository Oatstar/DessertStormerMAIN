using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<GameObject> allEnemies = new List<GameObject> { };
    [SerializeField] GameObject enemyContainer;

    [SerializeField] GameObject[] enemyPrefabs;

    Vector2 minSpawnPoint = new Vector2(-3f, -6f);
    Vector2 maxSpawnPoint = new Vector2(11f, 3f);

    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();


    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SpawnFloorMonsters(int floor)
    {
        occupiedPositions.Clear();

        for (int i = 0; i < floor; i++)
        {
            SpawnMonster(0);
            SpawnMonster(1);
        }
    }

    public void SpawnMonster(int enemyType)
    {
        Vector2 spawnPoint = GetRandomAvailableSpawnPoint();

        GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyType], spawnPoint, Quaternion.identity);
        spawnedEnemy.transform.SetParent(enemyContainer.transform);
        allEnemies.Add(spawnedEnemy);
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
        } while (occupiedPositions.Contains(spawnPoint) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Could not find an available spawn point after 100 attempts");
        }

        return spawnPoint;
    }

    Vector2 GetRandomSpawnPoint()
    {
        int randomX = Mathf.RoundToInt(Random.Range(minSpawnPoint.x, maxSpawnPoint.x));
        int randomY = Mathf.RoundToInt(Random.Range(minSpawnPoint.y, maxSpawnPoint.y));
        return new Vector2(randomX, randomY);
    }

    public bool IsPositionOccupied(Vector2 position)
    {
        return occupiedPositions.Contains(position);
    }

    public void EnemyDied(GameObject deadEnemy)
    {
        allEnemies.Remove(deadEnemy);
        if (allEnemies.Count <= 0)
            GameMasterManager.instance.AllEnemiesDead();
            
    }



}
