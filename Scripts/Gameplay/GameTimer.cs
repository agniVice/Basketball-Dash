using UnityEngine;

public class GameTimer : MonoBehaviour, IInitializable
{
    public static GameTimer Instance;

    public float DefaultTimer;
    public float Timer;

    private bool _isEnabled;

    private void Awake()
    {
        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this;
    }
    public void Initialize()
    {
        _isEnabled = true;

        ResetTimer();
    }
    private void FixedUpdate()
    {
        if (_isEnabled)
        {
            if (Timer > 0)
            {
                Timer -= Time.fixedDeltaTime;
            }
            else
            {
                ResetTimer();
                PlayerScore.Instance.AddScore();
            }
        }
    }
    public void ResetTimer()
    {
        Timer = DefaultTimer;
    }
    public void StartTimer()
    {
        _isEnabled  = true;
    }
}
