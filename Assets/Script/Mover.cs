using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("속도")]
    public float speed = 0.001f;

    void Update()
    {
        // speed 변수에 비례하여 부드럽게 이동하도록 수정
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
