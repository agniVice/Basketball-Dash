using UnityEngine;

public class GhostBall : MonoBehaviour
{
    public float Speed { get; private set; }

    [Header("Other settings")]

    [SerializeField] private Vector2[] _directions;

    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private Ball _ball;

    private float _timeToChangeDirection;
    private int _currentDirection = 2;

    // 0 - right on the top
    // 1 - right on the bottom
    // 2 - right to the down
    // 3 - right to the up

    private void Start()
    {
        _ball = Ball.Instance;

        ChangeDirection();
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        if (Vector2.Distance(transform.position, _ball.transform.position) <= 16)
            transform.position += new Vector3(1,0,0);

        Speed = _ball.Speed;

        transform.position += new Vector3(_directions[_currentDirection].x * Speed * Time.fixedDeltaTime, _directions[_currentDirection].y * Speed * Time.fixedDeltaTime, 0);
    }
    private void ChangeDirection()
    {
        _timeToChangeDirection = Random.Range(_minTime, _maxTime);

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

        Invoke("ChangeDirection", _timeToChangeDirection);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallTop"))
            _currentDirection = 0;
        if (collision.gameObject.CompareTag("WallBottom"))
            _currentDirection = 1;

        if (collision.gameObject.CompareTag("Obstacle"))
            Destroy(collision.gameObject);
    }
}
