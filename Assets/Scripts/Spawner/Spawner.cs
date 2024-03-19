using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModel
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private SpawnModel spawnModel = SpawnModel.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGo;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] float minRandomDelay;
    [SerializeField] float maxRandomDelay;

    private float _spawnTimer;
    private float _enemiesSpawned;

    private ObjectPooler _pooler;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
    }

    void Update()
    {
        _spawnTimer -=Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if(spawnModel==SpawnModel.Fixed)
        {
            delay = delayBtwSpawns;
        }else
        {
            delay = GetRandomDelay();
        }
        
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomRimer = Random.Range(minRandomDelay,maxRandomDelay);
        return randomRimer;
    }
}
