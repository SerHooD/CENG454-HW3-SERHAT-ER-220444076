using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int totalWaves = 3;

    private int _currentWave = 0;
    private int _enemiesAlive = 0;

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= HandleEnemyDeath;
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (_currentWave < totalWaves)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnWave());
            yield return new WaitUntil(() => _enemiesAlive <= 0);
            _currentWave++;
            GameEvents.WaveCompleted();
        }

        GameEvents.GameWon();
    }

    private IEnumerator SpawnWave()
    {
        _enemiesAlive = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = enemyPool.Get();
            enemy.transform.position = spawnPoint.position;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void HandleEnemyDeath(int totalKills)
    {
        _enemiesAlive--;
    }
}