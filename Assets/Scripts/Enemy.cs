using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;
    public int miniEnemySpawnCount = 0;
    public float spawnInterval = 2;
    public float speed;

    private float nextSpawn;
    private GameObject player;
    private Rigidbody enemyRb;
    private SpawnEnemy spawnEnemy;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnEnemy = FindObjectOfType<SpawnEnemy>();
        }
    }

    void FixedUpdate()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if (isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnEnemy.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("missile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}