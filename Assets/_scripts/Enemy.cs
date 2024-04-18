using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _target;
    [SerializeField] private float _speed;
    [SerializeField] private EnemyType _type;

    private List<Vector3> _checkpoints = new List<Vector3>();
    private int _currentCheckpointIndex = 0;
    private float _reachDistance = 1f;

    public EnemyType Type => _type;

    public UnityAction<Enemy> Destroying;

    private void Update()
    {
        if (_currentCheckpointIndex == _checkpoints.Count)
            return;

        Move();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) < _reachDistance)
        {
            _currentCheckpointIndex++;
            _target = _checkpoints[_currentCheckpointIndex];
        }
    }

    public void SetRoute(List<Vector3> checkpoints)
    {
        _checkpoints = checkpoints;
        _target = _checkpoints[_currentCheckpointIndex];
    }
}
