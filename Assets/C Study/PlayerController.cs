using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
  private int hp =100;

  private bool isJumping =false;

    void Start()
    {
        Debug.Log("게임시작! [Space] 점프, [A] 공격, [S] 방어");
;
    }

    void Update()
    {
        var key=Keyboard.current;
        if (key==null) return;

        if(key.spaceKey.wasPressedThisFrame)
        {
              Debug.Log("[Space] 점프");
            if(isJumping)
            {
              Debug.Log("공중에 떠 있습니다");
            }
            else
            {
              isJumping=true;
              Debug.Log("점프!");
        
            }
        }
        if(key.aKey.wasPressedThisFrame)
        {
              Debug.Log("[A] 공격");
           if(isJumping)
            {
              isJumping=false;
              Debug.Log("착지!");
            }        

              Debug.Log("공격!!!!!!!!!");
        }


        if(key.sKey.wasPressedThisFrame)
        {
              Debug.Log("[S] 방어");
              Debug.Log("체력상태: "+ GetHPstatus());
        }
        
        if(key.zKey.wasPressedThisFrame)
        {
          Debug.Log("[Z]데미지");
          int damage =25;
          Takedamage(damage);
          Debug.Log("데미지 공격:"+damage);
          Debug.Log("체력상태:" + GetHPStatus());
        }
    }
    private void Takedamage(int damage)
    {
      hp = hp - damage;
      if(hp< 0) hp = 0;
    }

    private void GetHPStatus()
    {
      if(hp >=70)
      {
        return "건강함";
      }
      else if (hp>=30)
      {
        return "위험!";
      }
      else 
      {
        return "빈사!";
      }
    }


}   