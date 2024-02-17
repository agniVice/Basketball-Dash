using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Other settings")]
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [Space]
    [Header("Difficulty 1")]
    [SerializeField] private int _minCount1;
    [SerializeField] private int _maxCount1;

    [Space]
    [Header("Difficulty 2")]
    [SerializeField] private int _maxCount2;
    [SerializeField] private int _minCount2;

    [Space]
    [Header("Difficulty 3")]
    [SerializeField] private int _minCount3;
    [SerializeField] private int _maxCount3;

    [Space]
    [Header("Difficulty 4")]
    [SerializeField] private int _minCount4;
    [SerializeField] private int _maxCount4;

    [Space]
    [Header("Difficulty 5")]
    [SerializeField] private int _minCount5;
    [SerializeField] private int _maxCount5;

    private List<GameObject> _obstacles = new List<GameObject>();

    private void Start()
    {
        Clear();
    }
    public void Spawn()
    {
        for (int i = 0; i < GetRandomCount(); i++)
        {
            var obstacle = Instantiate(obstaclePrefab, GetRandomPosition(), Quaternion.identity, transform);
            _obstacles.Add(obstacle);
        }
    }
    public void Clear()
    {
        foreach (GameObject obstacle in _obstacles)
        {
            if(obstacle != null)
                Destroy(obstacle);
        }
        _obstacles.Clear();
    }
    private Vector2 GetRandomPosition()
    {
        return new Vector2(transform.position.x, Random.Range(minY, maxY));
    }
    private int GetRandomCount()
    {
        switch (LevelManager.Instance.LevelDifficulty)
        {
            case 0:
                return Random.Range(_minCount1, _maxCount1);
            case 1:
                return Random.Range(_minCount2, _maxCount2);
            case 2:
                return Random.Range(_minCount3, _maxCount3);
            case 3:
                return Random.Range(_minCount4, _maxCount4);
            case 4:
                return Random.Range(_minCount5, _maxCount5);
        }
        return Random.Range(_minCount1, _maxCount5);
    }

    /*private int GetRandomCount()
    {
        switch (LevelManager.Instance.LevelDifficulty)
        {
            case 0:
                return Random.Range(_minCount1, _maxCount1);
            case 1:
                return Random.Range(_minCount2, _maxCount2);
            case 2:
                return Random.Range(_minCount3, _maxCount3);
            case 3:
                return Random.Range(_minCount4, _maxCount4);
            case 4:
                return Random.Range(_minCount5, _maxCount5);
        }
        return Random.Range(_minCount1, _maxCount5);
    }*/
}
