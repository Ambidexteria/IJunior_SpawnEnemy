using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.Destroying?.Invoke(enemy);
    }
}
