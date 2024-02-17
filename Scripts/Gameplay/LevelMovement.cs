using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    public static LevelMovement Instance;

    public SpriteRenderer lastElement;

    public GameObject[] SpawnersPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Invoke("FirstSpawn", 0.5f);
    }
    private void FirstSpawn()
    {
        lastElement.GetComponent<LevelElement>().SpawnObstacles();
    }
}
