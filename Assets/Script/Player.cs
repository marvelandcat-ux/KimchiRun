using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid; // 물리 효과를 주기 위한 변수
    
    public float jumpForce = 6f; // 점프하는 힘 (Unity 인스펙터에서 수정 가능)
    bool isJumping = false; // 현재 점프 중인지 확인하는 변수(Variable)

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>(); // 점프를 위해 Rigidbody2D 가져오기
    }

    void Update()
    {
        // 땅에 있고(점프 중이 아닐 때) 스페이스바를 누르면
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true; // 점프 중이라고 표시
            animator.SetInteger("state", 1); // 애니메이션을 1(점프)로 변경
            
            // 위로 솟아오르는 물리적인 힘을 가합니다.
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // 2D 물리 엔진에서 다른 물체와 충돌(착지)했을 때
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 등에 닿아서 착지하면 다시 점프가 가능하도록 false로 변경
        isJumping = false; 
        
        // 애니메이션을 2(착지/대기)로 변경
        animator.SetInteger("state", 2);
    }
}
