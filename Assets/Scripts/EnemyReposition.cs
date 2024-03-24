using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This script is possibly a temporary script while we decide in which script this functionality should be placed in.


public class EnemyReposition : MonoBehaviour
{
    public float despawnDistance = 15f;
    Transform player;
    


    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
