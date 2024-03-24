using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // List of groups of enemies to spawn on this wave
        public int enemyWaveLimit; // Total number of enemies allowed on this wave.
        public float spawnInterval; // Enemy spawn interval.
        public int totalSpawnCount; // Total number of enemies spawned on this wave. 
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemySpawnQuota; // Number of enemies of this type that need to be spawned for this wave
        public int enemySpawnCount; // Number of enemies of this type spawned in this wave. 
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // List of all the waves in the game.
    public int currentWaveIndex;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        CalculateWaveQuota();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemySpawnQuota; 
        }
        waves[currentWaveIndex].enemyWaveLimit = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        // Check if minimum no. of enemies in this wave has been spawned
        if (waves[currentWaveIndex].totalSpawnCount < waves[currentWaveIndex].enemyWaveLimit)
        {
            // Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
            {
                // Check if the minimum no. of this enemy type has been spawned
                if (enemyGroup.enemySpawnCount < enemyGroup.enemySpawnQuota)
                {
                    Vector2 spawnPosition = new(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.enemySpawnCount++;
                    waves[currentWaveIndex].totalSpawnCount++;
                }
            }
        }
    }
}
