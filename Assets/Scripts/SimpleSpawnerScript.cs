using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab1; // Assign in inspector
    public GameObject enemyPrefab2; // Assign in inspector
    
    public float spawnInterval = 2f; // Time between spawns
    private float timer; // Timer to track spawn timing

    void Start()
    {
        // Initialize the timer to the spawn interval for an initial spawn
        timer = spawnInterval;
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer <= 0)
        {
            SpawnEnemy();
            // Reset the timer, optionally you can add randomness here
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // Choose randomly between the two enemies
        GameObject enemyToSpawn = Random.Range(0, 2) == 0 ? enemyPrefab1 : enemyPrefab2;
        
        // Set spawn position off the side of the screen
        // You might need to adjust the values based on your game's camera and design
        Vector3 spawnPosition = new Vector3(Screen.width + 100, Random.Range(-Screen.height, Screen.height), 0);
        
        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0; // Ensure spawned enemy is visible (assuming your game is on the XY plane)

        // Instantiate the enemy at the spawn position
        Instantiate(enemyToSpawn, worldPosition, Quaternion.identity);
    }
}
