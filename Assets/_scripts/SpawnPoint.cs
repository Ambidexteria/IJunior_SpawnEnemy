using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private EnemyType _type;

    public EnemyType Type => _type;
    public Target Target => _target;
}
