using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TakeDamage(10);
    }

    // Update is called once per frame
    public void TakeDamage(int damage) 
    {    
        Debug.Log("damage"+ damage);
        return;
    }
    
}
