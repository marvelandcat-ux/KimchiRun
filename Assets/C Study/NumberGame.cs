using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberGame : MonoBehaviour
{
    int tryCount = 0;
    void Start()
    {
        Debug.Log("--------------------");
        Debug.Log("숫자 맞추기 게임을 시작하겠다.");
        Debug.Log("1~5사이의 숫자를 맞춰보세요");
        Debug.Log("[1].[2],[3].[4],[5]키를 누르세요");
        Debug.Log("--------------------");
       
    }

    // Update is called once per frame
    void Update()
    {
        int input = -1;

        if(Input.GetKeyDown(KeyCode.Alpha1)) input =1;
        if(Input.GetKeyDown(KeyCode.Alpha2)) input =2;
        if(Input.GetKeyDown(KeyCode.Alpha3)) input =3;
        if(Input.GetKeyDown(KeyCode.Alpha4)) input =4;
        if(Input.GetKeyDown(KeyCode.Alpha5)) input =5;

        if(input != -1)
        {
            CheckAnswer(input);
        }
    }

    private void CheckAnswer(int input)
    {
       
        tryCount++;
        Debug.Log($"입력: {input}, 시도횟수: {tryCount}");
        if(tryCount > 5)
        {
            Debug.Log("실패!");
            
        }


    }
}
