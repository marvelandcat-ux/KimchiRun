using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("속도")]
    public float speed = 0.001f;

    void Update()
    {
        float moveSpeed = GameManager.instance.CalculateGameSpeed();
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
