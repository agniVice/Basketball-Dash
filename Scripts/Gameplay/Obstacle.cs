using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Sprite[] _obstacleSprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GetRandomSprite();
    }
    private Sprite GetRandomSprite()
    {
        return _obstacleSprites[Random.Range(0, _obstacleSprites.Length)];
    }
}
