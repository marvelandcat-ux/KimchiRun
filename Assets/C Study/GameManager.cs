using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Clear
}
public class GameManager : MonoBehaviour
{
    public EnemyController enemyController;
    private GameState currentState = GameState.Playing;
    public static int gameState = 0;

    void Update()
    {
        Keyboard curKey = Keyboard.current;
        if (curKey != null && curKey.pKey.wasPressedThisFrame)
        {
            currentState = GameState.Paused;
            
        }
        if (curKey != null && curKey.oKey.wasPressedThisFrame)
        {
            currentState = GameState.GameOver;
        }
    
        switch (currentState)
        {
            case GameState.Playing:
                Debug.Log("게임진행 중");
                break;
            case GameState.Paused:
                Debug.Log("게임멈춤");
                break;
            case GameState.GameOver:
                Debug.Log("게임오버");
                break;
            case GameState.Clear:
                Debug.Log("클리어!");
                break;
        }
    }
}
