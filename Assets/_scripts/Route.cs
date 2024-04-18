using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private List<Transform> _checkpoints = new List<Transform>();

    private List<Vector3> _checkpointsPositions = new List<Vector3>();

    private void Awake()
    {
        ConvertCheckpointsPositions();
    }

    public List<Vector3> GetCheckpoints()
    {
        return new List<Vector3>(_checkpointsPositions);
    }

    private void ConvertCheckpointsPositions()
    {
        foreach (var checkpoint in _checkpoints)
            _checkpointsPositions.Add(checkpoint.position);
    }
}
