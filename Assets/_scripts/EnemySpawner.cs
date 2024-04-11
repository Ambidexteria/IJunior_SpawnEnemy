using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const string SpawnPointTag = "SpawnPoint";

    [SerializeField] bool _coroutineActive;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Enemy> _enemies;

    private WaitForSeconds _wait;

    private void Awake()
    {
        FindAllSpawnPoints();
    }

    private void Start()
    {
        _coroutineActive = true;
        StartCoroutine(SpawnCoroutine());
    }

    private void FindAllSpawnPoints()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(SpawnPointTag);

        foreach(var spawnPoint in spawnPoints)
            _spawnPoints.Add(spawnPoint.transform);
    }

    private IEnumerator SpawnCoroutine()
    {
        _wait = new WaitForSeconds(_spawnDelay);

        while(_coroutineActive)
        {
            Spawn();
            yield return _wait;
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

        Enemy spawnedEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemy.SetDirection(GetRandomDirection());
        spawnedEnemy.Destroying += Destroy;

        _enemies.Add(spawnedEnemy);
    }

    private void Destroy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 direction = new Vector3();
        direction.x = Random.Range(-1f, 1f);
        direction.z = Random.Range(-1f, 1f);

        return direction.normalized;
    }
}
