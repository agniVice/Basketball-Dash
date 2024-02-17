using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour, ISubscriber
{
    public static Ball Instance;

    public float Speed { get; private set; }

    [Header("Other settings")]

    [SerializeField] private GameObject _particlePrefab;

    [SerializeField] private Vector2[] _directions;

    [Space]
    [Header("Speed settings")]

    [SerializeField] private float _speed;
    [SerializeField] private float _accelerate;
    [SerializeField] private float _maxSpeed;

    private TrailRenderer _trailRenderer;

    private int _currentDirection = 2;

    // 0 - right on the top
    // 1 - right on the bottom
    // 2 - right to the down
    // 3 - right to the up

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Speed = _speed;
    }
    private void Start()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        transform.position += new Vector3(_directions[_currentDirection].x * Speed * Time.fixedDeltaTime, _directions[_currentDirection].y * Speed * Time.fixedDeltaTime, 0);
    }
    public void SubscribeAll()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
        GameState.Instance.ScoreAdded += CheckMySpeed;
    }
    public void UnsubscribeAll()
    {
        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
        GameState.Instance.ScoreAdded -= CheckMySpeed;
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void CheckMySpeed()
    {
        if (Speed > _maxSpeed)
            return;

        Speed += _accelerate;
    }
    private void ChangeDirection()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Swap, Random.Range(0.95f, 1.1f), 0.75f);

        switch (_currentDirection)
        {
            case 0:
                _currentDirection = 2;
                break;
            case 1:
                _currentDirection = 3;
                break;
            case 2:
                _currentDirection = 3;
                break;
            case 3:
                _currentDirection = 2;
                break;
        }
    }
    private void OnPlayerMouseDown()
    {
        ChangeDirection();
    }
    private void DestroyBall()
    {
        GameState.Instance.FinishGame();
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Lose, 1f);

        transform.DOScale(0, 0.1f).SetLink(gameObject);

        SpawnParticle();

        _trailRenderer.time = 0.2f;

        SceneLoader.Instance.LoadWithDelay("Gameplay", 1f);

        Destroy(gameObject, 0.2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallTop"))
            _currentDirection = 0;
        if (collision.gameObject.CompareTag("WallBottom"))
            _currentDirection = 1;

        if (collision.gameObject.CompareTag("Obstacle"))
            DestroyBall();
    }
}
