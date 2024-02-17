using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private int _difficulty;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.SetDifficulty(_difficulty));
    }
}
