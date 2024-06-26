using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool _coroutineActive;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private EnemyFactory _enemyFactory;

    private WaitForSeconds _wait;

    private void OnEnable()
    {
        if (_enemies != null)
            SubscribeOnEnemies();
    }

    private void Start()
    {
        _coroutineActive = true;
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        if (_enemies != null)
            UnsubscribeOnEnemies();
    }

    private IEnumerator SpawnCoroutine()
    {
        _wait = new WaitForSeconds(_spawnDelay);

        while (_coroutineActive)
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
        spawnedEnemy.SetTarget(point.Target);
        spawnedEnemy.Destroying += DestroyEnemy;

        _enemies.Add(spawnedEnemy);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        if(enemy == null)
            throw new System.ArgumentNullException(nameof(DestroyEnemy) + " " + nameof(enemy));

        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SubscribeOnEnemies()
    {
        foreach (var enemy in _enemies)
            enemy.Destroying += DestroyEnemy;
    }

    private void UnsubscribeOnEnemies()
    {
        foreach (var enemy in _enemies)
            enemy.Destroying -= DestroyEnemy;
    }
}
