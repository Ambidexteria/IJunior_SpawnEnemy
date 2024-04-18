using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const string SpawnPointTag = "SpawnPoint";

    [SerializeField] bool _coroutineActive;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private EnemyFactory _enemyFactory;

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
        {
            if(spawnPoint.TryGetComponent(out SpawnPoint point))
            _spawnPoints.Add(point);
        }
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
        SpawnPoint point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Enemy spawnedEnemy = _enemyFactory.Create(point.Type);

        Vector3 spawnPosition = point.transform.position;
        spawnedEnemy.transform.position = spawnPosition;

        spawnedEnemy.transform.SetParent(transform, true);
        spawnedEnemy.SetRoute(point.Route.GetCheckpoints());
        spawnedEnemy.Destroying += Destroy;

        _enemies.Add(spawnedEnemy);
    }

    private void Destroy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
