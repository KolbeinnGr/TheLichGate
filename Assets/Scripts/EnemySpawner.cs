using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// Todo:
/// * Finish spawning all enemy quota first before moving onto next wave? Have to discuss with team later.
/// * Better organization and hiding stats that aren't needed for clarity purposes.
/// * Fix enemies spawning +1 more than allowed.
///     Bug Example: Max enemies allowed at once is 4. Script spawns 5 enemies, and then stops spawning until it goes to 3 enemies or lower.

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // List of groups of enemies to spawn on this wave
        public int enemyWaveQuota; // Minimum number of enemies needed to spawn on this wave.
        public float spawnInterval; // Enemy spawn interval.
        public int totalSpawnCount; // Total number of enemies spawned on this wave. 
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemySpawnQuota; // Minimum no. of enemies of this type that need to be spawned for this wave

        [HideInInspector]
        public int enemySpawnCount; // Number of enemies of this type spawned in this wave. 

        public GameObject enemyPrefab;
        public int modifiedHealth; // For enemies that need a different health value for a wave.
    }

    public List<Wave> waves; // List of all the waves in the game.
    public int currentWaveIndex;

    [Header("Spawner Attributes")]
    float spawnTimer; // Timer that determines when the next enemy spawns.
    public int allEnemiesAlive; // The count of every enemy alive
    public int enemiesAliveInWave; // The count of every enemy alive that is from the current wave.
    public int maxEnemiesAllowed; // Max enemies allowed on the field.
    public bool maxEnemiesReached = false;
    public float waveLength = 50; // The length of each wave.
    public float waveInterval = 10; // The interval between each wave.
    private bool canChangeWave = true;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // A list of places for the enemies to spawn relative to the player. WILL FIX LATER
    Transform player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        CalculateWaveQuota();
        GameManager.Instance.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
       
        // Checks if the current wave has ended to start the next wave.
        if (currentWaveIndex < waves.Count && canChangeWave == true && time.Seconds >= waveLength)
        {
            StartCoroutine(BeginNextWave());
            canChangeWave = false;
        }

        // Check if it's time to spawn the next enemy.
        if (time.TotalSeconds % waves[currentWaveIndex].spawnInterval == 0)
        {
            SpawnEnemies();
        }
    }

    /// <summary>
    /// Checks if the minimum enemy quota has been reached and calls SpawnAllEnemies() if not. Then waits before starting the next wave depending on wave interval.
    /// </summary>
    IEnumerator BeginNextWave()
    {
        if (waves[currentWaveIndex].totalSpawnCount < waves[currentWaveIndex].enemyWaveQuota)
        {
            SpawnAllEnemies();
            enemiesAliveInWave = 0;
        }

        yield return new WaitForSeconds(waveInterval);

        if(currentWaveIndex < waves.Count - 1)
        {
            currentWaveIndex++;
            CalculateWaveQuota();
            canChangeWave = true;
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemySpawnQuota; 
        }
        waves[currentWaveIndex].enemyWaveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    /// <summary>
    /// This method will stop spawning enemies if the amount of enemies on the map is maximum.
    /// This method will only spawn enemies in a particular wave until it is time for the next wave's enemies to be spawned.
    /// </summary>
    void SpawnEnemies()
    {
        // Check if minimum no. of enemies in this wave has been spawned
        if (waves[currentWaveIndex].totalSpawnCount < waves[currentWaveIndex].enemyWaveQuota && !maxEnemiesReached)
        {
            // Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
            {
                // Check if the minimum no. of this enemy type has been spawned
                if (enemyGroup.enemySpawnCount < enemyGroup.enemySpawnQuota)
                {
                    if(enemiesAliveInWave >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                    }
                    // Spawning enemies at random positions
                    GameObject newEnemy;
                    newEnemy = Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[UnityEngine.Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    if (enemyGroup.modifiedHealth != 0)
                    {
                        newEnemy.GetComponent<Health>().maxHealth = enemyGroup.modifiedHealth;
                    }

                    enemyGroup.enemySpawnCount++;
                    waves[currentWaveIndex].totalSpawnCount++;
                    allEnemiesAlive++;
                    enemiesAliveInWave++;
                }
            }
        }

        if (enemiesAliveInWave < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    /// <summary>
    /// This method will spawn every enemy that hasn't reached their minimum quota at the end of the wave.
    /// </summary>
    void SpawnAllEnemies()
    {
    // Check if minimum no. of enemies in this wave has been spawned

        // Spawn each type of enemy until the quota is filled
        foreach (var enemyGroup in waves[currentWaveIndex].enemyGroups)
        {
            // Check if the minimum no. of this enemy type has been spawned
            if (enemyGroup.enemySpawnCount < enemyGroup.enemySpawnQuota)
            {
                // Spawning enemies at random positions
                Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[UnityEngine.Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                enemyGroup.enemySpawnCount++;
                waves[currentWaveIndex].totalSpawnCount++;
                allEnemiesAlive++;
            }
        }
        enemiesAliveInWave = 0;
    }

    //Call this function when an enemy is killed.
    public void OnEnemyKilled()
    {
        allEnemiesAlive--;
        enemiesAliveInWave--;
    }
}
