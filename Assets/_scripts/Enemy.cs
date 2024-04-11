using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;

    public UnityAction<Enemy> Destroying;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _direction, Space.World);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
