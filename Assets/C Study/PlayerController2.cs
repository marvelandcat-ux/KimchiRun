using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    void Update()
    {
       var keyboard = Keyboard.current;
       if (keyboard == null) return;

       if (keyboard.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("스페이스바를 눌렀습니다!");
        }

        if (keyboard.wKey.isPressed)
        {
            Debug.Log("W키를 누를는 중...");
        }
    }   
}    