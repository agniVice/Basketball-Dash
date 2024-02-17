using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;

    [SerializeField] private float _distanceDifference;

    private bool _moving = true;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;
        if (!_moving)
            return;

        Move();
    }
    private void Move()
    {
        Vector2 ballPosition = Ball.Instance.transform.position;
        transform.position = new Vector3(ballPosition.x + _distanceDifference, transform.position.y, transform.position.z);
    }
}
