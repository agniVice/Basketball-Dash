using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int LevelDifficulty { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty(int difficulty)
    {
        if (LevelDifficulty == difficulty)
            return;
            
        LevelDifficulty = difficulty;

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Swap, 1f);

        FindObjectOfType<MenuUserInterface>().UpdateLevelDifficulty(LevelDifficulty);
    }
}
