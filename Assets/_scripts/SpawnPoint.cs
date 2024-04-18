using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Route _route;
    [SerializeField] private Transform _target;
    [SerializeField] private EnemyType _type;

    public EnemyType Type => _type;
    public Vector3 Target => _target.position;
    public Route Route => _route;
}
