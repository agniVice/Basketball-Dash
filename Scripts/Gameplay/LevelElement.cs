using UnityEngine;

public class LevelElement : MonoBehaviour
{
    [SerializeField] private SpawnerManager _spawnerManager;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize();
    }
    private void Initialize()
    {
        _spawnerManager = Instantiate(LevelMovement.Instance.SpawnersPrefab[LevelManager.Instance.LevelDifficulty]).GetComponent<SpawnerManager>();
        _spawnerManager.Initialize(transform);
    }
    private void Respawn()
    {
        ClearObstacles();
        transform.position = new Vector3(LevelMovement.Instance.lastElement.transform.position.x + _spriteRenderer.sprite.bounds.size.x, 0f, 0f);
        SpawnObstacles();
    }
    public void ClearObstacles()
    {
        _spawnerManager.ClearObstacles();
    }
    public void SpawnObstacles()
    {
        _spawnerManager.SpawnObstacles();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Camera"))
        {
            Respawn();
            LevelMovement.Instance.lastElement = _spriteRenderer;
        }
    }
}
