using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerController pc;

    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float opCoolDown;
    public float opCoolDownDur;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       ChunckChecker();
       ChunkSaver(); 
    }

    void ChunckChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        // Right
        if(pc.direction.x > 0 && pc.direction.y == 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                    SpawnChunk();
                }
            }
        }
        // Left
        if(pc.direction.x < 0 && pc.direction.y == 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                    SpawnChunk();
                }
            }
        }
        // Up
        if(pc.direction.x == 0 && pc.direction.y > 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                    SpawnChunk();
                }                
            }
        }
        // Down
        if(pc.direction.x == 0 && pc.direction.y < 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                    SpawnChunk();
                }
            }
        }
        // Right Up
        if(pc.direction.x > 0 && pc.direction.y > 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Up").position;
                    SpawnChunk();
                }
            }
        }
        // Right Down
        if(pc.direction.x > 0 && pc.direction.y < 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Down").position;
                    SpawnChunk();
                }
            }
        }
        // Left Up
        if(pc.direction.x < 0 && pc.direction.y > 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Up").position;
                    SpawnChunk();
                }
            }
        }
        // Left Down
        if(pc.direction.x < 0 && pc.direction.y < 0)
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                SpawnChunk();
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left").position;
                    SpawnChunk();
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Down").position;
                    SpawnChunk();
                }
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkSaver()
    {
        opCoolDown -= Time.deltaTime;

        if(opCoolDown <= 0f)
        {
            opCoolDown = opCoolDownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
