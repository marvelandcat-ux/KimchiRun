using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Lives = 3;
    public Player player;
    public GameObject player2;
    private void Awake()

    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instance created : " + instance.name);
        }
        else
        {
            Debug.Log("GameManager instance Late Instance : " + instance.name);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Plater's Lives" + player.lives);
            Debug.Log("Plater's Lives" + player2.GetComponent<Player>().lives);
            Lives = 1;
            SceneManager.LoadScene("Score");
        }
    }
}
