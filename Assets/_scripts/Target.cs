using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Route _route;

    private List<Vector3> _checkpoints = new List<Vector3>();
    private float _reachDistance = 1f;
    private int _currentCheckpointIndex = 0;
    private Vector3 _currentTarget;

    private void Start()
    {
        if (_route == null)
            throw new System.ArgumentNullException(nameof(_route));

        SetRoute();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentTarget) < _reachDistance)
        {
            _currentCheckpointIndex++;
            _currentCheckpointIndex %= _checkpoints.Count;
            _currentTarget = _checkpoints[_currentCheckpointIndex];
        }
    }

    private void SetRoute()
    {
        _checkpoints = _route.GetCheckpoints();
        _currentTarget = _checkpoints[_currentCheckpointIndex];
    }
}
