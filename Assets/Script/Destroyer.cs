using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Camera maincamera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        maincamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float cameraLeftEdge = maincamera.transform.position.x -
        maincamera.orthographicSize * maincamera.aspect;
        float objectRightedge = spriteRenderer.bounds.max.x;

        if (objectRightedge < cameraLeftEdge)
        {
            Destroy(gameObject);
        }


    }
}
