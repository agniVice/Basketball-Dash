using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<ObstacleSpawner> _spawners;

    private Transform _trackObject;

    public void Initialize(Transform trackObject)
    { 
        _trackObject = trackObject;
    }
    private void FixedUpdate()
    {
        transform.position = _trackObject.position;
    }
    public void SpawnObstacles()
    {
        foreach(ObstacleSpawner spawner in _spawners)
            spawner.Spawn();
    }
    public void ClearObstacles()
    {
        foreach(ObstacleSpawner spawner in _spawners)
            spawner.Clear();
    }
}
