using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private EnemyType _type;

    private Target _currentTarget;

    public EnemyType Type => _type;

    public UnityAction<Enemy> Destroying;

    private void Update()
    {
        Move();
    }

    public void SetTarget(Target target)
    {
        if(target == null)
            throw new ArgumentNullException($"{nameof(Enemy)} {nameof(SetTarget)} {nameof(target)}");

        _currentTarget = target;
    }

    private void Move()
    {
        transform.LookAt(_currentTarget.transform);
        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, _speed * Time.deltaTime);
    }
}
