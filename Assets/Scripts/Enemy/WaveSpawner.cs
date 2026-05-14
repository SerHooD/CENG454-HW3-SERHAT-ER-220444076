using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private ObjectPool flankerPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int totalWaves = 2;

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
            
            bool isFlanker = (i % 2 != 0);
            ObjectPool pool = isFlanker ? flankerPool : enemyPool;
            
            GameObject enemyObj = pool.Get();
            enemyObj.transform.position = spawnPoint.position;
            
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.Init(pool);

            IMovementStrategy strategy = isFlanker
                ? (IMovementStrategy)new FlankMoveStrategy(3f)
                : new DirectMoveStrategy(3f);
            
            enemy.SetStrategy(strategy);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void HandleEnemyDeath(int totalKills)
    {
        _enemiesAlive--;
    }
}