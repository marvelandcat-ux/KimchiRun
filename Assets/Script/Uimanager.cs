using TMPro;
using UnityEngine;

public class Uimanager : MonoBehaviour
{
    public static Uimanager instance;

    public GameObject IntroUI;
    public GameObject ItemSpawner;

    public TMP_Text ScoreText;
    public TMP_Text HighScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        IntroUI.SetActive(true);
        ItemSpawner.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            ScoreText.text = "Score:" + GameManager.instance.CalculateScore();
            HighScore.text = "High Score: " + GameManager.instance.GetHighScore();
        }
        else
        {
            ScoreText.text = "";
            HighScore.text = "";
        }
    }

}
