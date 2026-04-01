using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberGame : MonoBehaviour
{
    private int answer = 3;
    private const int Maxtry = 5;
    private int tryCount = 0;
    private bool isOver = false;

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
        if (isOver) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) input = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) input = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) input = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) input = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) input = 5;

        if (input != -1)
        {
            CheckAnswer(input);
        }
    }

    private void CheckAnswer(int input)
    {

        tryCount++;
        Debug.Log($"입력: {input}, 시도횟수: {tryCount}");
        if (input == answer)
        {
            Debug.Log($"정답입니다!{tryCount}번 만에맞췄습니다.");
            isOver=true;
            return;
        }

        if (tryCount >= Maxtry)
        {
            Debug.Log($"실패 정답은{answer}이였습니다! ");
            isOver=true;
            return;
        }
        if (input > answer)
        {
            Debug.Log("더 작은 숫자입력해주세요");
        }
        else
        {
            Debug.Log("더 큰 숫자를 입력해주세요");
        }
    }
}
