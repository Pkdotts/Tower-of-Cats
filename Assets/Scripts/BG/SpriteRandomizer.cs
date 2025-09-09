using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        if (sprites.Length > 0)
        {
            SpriteRenderer spr = GetComponent<SpriteRenderer>();
            spr.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}