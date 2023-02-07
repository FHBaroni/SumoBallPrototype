using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnRange = 9.0f;
    // Start is called before the first frame update
    void Start()
    {        
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private  Vector3 GenerateSpawnPosition()
    {
        float spawnXPosition = Random.Range(-spawnRange, spawnRange);
        float spawnZPosition = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnXPosition, 0, spawnZPosition);
        return randomPos;
    }
}
