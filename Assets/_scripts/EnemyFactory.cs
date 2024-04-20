using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] List<Enemy> _enemiesPrefabs;

    private Dictionary<EnemyType, Enemy> _enemies = new Dictionary<EnemyType, Enemy>();

    private void Awake()
    {
        foreach(Enemy enemy in _enemiesPrefabs)
        {
            if (_enemies.ContainsKey(enemy.Type))
                throw new ArgumentException(nameof(EnemyFactory) + " " + enemy.Type.ToString());

            _enemies.Add(enemy.Type, enemy);
        }
    }

    public Enemy Create(EnemyType type)
    {
        return Instantiate(_enemies[type]);
    }
}
