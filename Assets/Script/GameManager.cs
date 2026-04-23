
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Gameover
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState = GameState.Intro;

    public int lives = 3;
    private bool isGameover = false;

    private float playStartTime;
    public int HighScore = 0;
    public int MyScore;

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
        if (gameState == GameState.Intro)
        {
            isGameover = false;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Debug.Log("스페이스바 눌림");
                gameState = GameState.Playing;
                Uimanager.instance.IntroUI.SetActive(false);
                Uimanager.instance.ItemSpawner.SetActive(true);

                playStartTime = Time.time;
            }
        }

        else if (gameState == GameState.Playing)
        {
            if (lives == 0)
            {
                gameState = GameState.Gameover;
                Uimanager.instance.ItemSpawner.SetActive(false);
                SaveScore();
            }
        }
        else if (gameState == GameState.Gameover)
        {
            if (isGameover == false)
            {
                Invoke("GameoverEvent", 3f);
            }
            isGameover = true;

        }
    }
    private void SaveScore()
    {

        MyScore = CalculateScore();
        int HighScore = GetHighScore();

        if (MyScore > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", MyScore);
            PlayerPrefs.Save();
        }
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    public int CalculateScore()
    {
        int score = Mathf.FloorToInt(Time.time - playStartTime);
        return score;
    }

    private void GameoverEvent()
    {
        lives = 3;
        gameState = GameState.Intro;
        SceneManager.LoadScene("Main");
    }
    public void AddLiveHasLimit()
    {
        lives = Mathf.Min(lives + 1, 3);
    }

    public void RemoveLive()
    {
        lives--;
    }

    public void GameOver()
    {
        gameState = GameState.Gameover;
        Uimanager.instance.ItemSpawner.SetActive(false);
    }

    public float CalculateGameSpeed()
    {
        if (gameState != GameState.Playing)
        {
            return 5f;
        }
        float speed = 5f + CalculateScore() * 0.05f;
        return Mathf.Min(speed, 30f);
    }
}
