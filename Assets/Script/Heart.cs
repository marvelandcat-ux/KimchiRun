using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite HeartOn;
    public Sprite HeartOff;
    private SpriteRenderer spriteRenderer;
    public int LiveNumber;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.instance.lives >= LiveNumber)
        {
            spriteRenderer.sprite = HeartOn;
        }
        else
        {
            spriteRenderer.sprite = HeartOff;
        }
    }
}
