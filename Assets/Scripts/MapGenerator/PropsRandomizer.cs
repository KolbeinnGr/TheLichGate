using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    public List<int> propWeights; // List of weights for each prefab

    // Start is called before the first frame update
    void Start()
    {
        if (propPrefabs.Count != propWeights.Count) {
            Debug.LogError("PropPrefabs and PropWeights lists must be of the same size.");
            return;
        }

        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach(GameObject spawnpoint in propSpawnPoints)
        {
            GameObject prop = Instantiate(ChooseWeightedPrefab(), spawnpoint.transform.position, Quaternion.identity);
            prop.transform.parent = spawnpoint.transform;
        }
    }

    GameObject ChooseWeightedPrefab()
    {
        int totalWeight = 0;
        foreach (int weight in propWeights) {
            totalWeight += weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int sum = 0;

        for (int i = 0; i < propPrefabs.Count; i++) {
            sum += propWeights[i];
            if (randomWeight < sum) {
                return propPrefabs[i];
            }
        }

        // Fallback, should not really happen
        return propPrefabs[propPrefabs.Count - 1];
    }
}
