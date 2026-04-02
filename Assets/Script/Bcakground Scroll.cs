using UnityEngine;

public class BcakgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        material.mainTextureOffset = new Vector2(offset, 0);
    }
}
