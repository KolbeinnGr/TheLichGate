using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector3 playerLastPosition;
    string secondDirection;
    string thirdDirection;

    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float opCoolDown;
    public float opCoolDownDur;
    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
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
        Vector3 direction = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(direction);
        if (directionName == "Left")
        {
            secondDirection = "Left Down";
            thirdDirection = "Left Up";
        }
        if (directionName == "Right")
        {
            secondDirection = "Right Down";
            thirdDirection = "Right Up";
        }
        if (directionName == "Up")
        {
            secondDirection = "Right Up";
            thirdDirection = "Left Up";
        }
        if (directionName == "Down")
        {
            secondDirection = "Left Down";
            thirdDirection = "Right Down";
        }
        if (directionName == "Left Up")
        {
            secondDirection = "Left";
            thirdDirection = "Up";
        }
        if (directionName == "Right Up")
        {
            secondDirection = "Right";
            thirdDirection = "Up";
        }
        if (directionName == "Left Down")
        {
            secondDirection = "Down";
            thirdDirection = "Left";
        }
        if (directionName == "Right Down")
        {
            secondDirection = "Down";
            thirdDirection = "Right";
        }
            
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask) || !Physics2D.OverlapCircle(currentChunk.transform.Find(secondDirection).position, checkRadius, terrainMask) || !Physics2D.OverlapCircle(currentChunk.transform.Find(thirdDirection).position, checkRadius, terrainMask))
        {
            if (directionName.Contains("Up") && directionName.Contains("Right"))
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Up").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right").position);
                }
            }

            else if (directionName.Contains("Up") && directionName.Contains("Left"))
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Up").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left").position);
                }
            }

            else if (directionName.Contains("Down") && directionName.Contains("Right"))
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Down").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right").position);
                }
            }

            else if (directionName.Contains("Down") && directionName.Contains("Left"))
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Down").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left").position);
                }
            }

            else if (directionName == "Right")
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right Down").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right Up").position);
                }
            }

            else if (directionName == "Left")
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left Down").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left Up").position);
                }
            }

            else if (directionName == "Up")
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right Up").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left Up").position);
                }
            }

            else if (directionName == "Down")
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find(directionName).position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Left Down").position);
                }
                if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkRadius, terrainMask))
                {
                    SpawnChunk(currentChunk.transform.Find("Right Down").position);
                }
            }
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //Moving horizontally more than vertically
            if(direction.y > 0.5f)
            {
                // Also moving upwards
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                // Also moving Downwards
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                // Moving straight horizontally
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            //Moving vertically more than horizontally
            if(direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
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
