using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    void Start()
    {   
        //배열
        string[] enemies = {"슬라임","고블린","오크","나쁜년"};

        Debug.Log(enemies[1]);
        Debug.Log(enemies[2]);
       
        


        for(int i =0; i < enemies.Length; i++)
        {
            Debug.Log(enemies [i] );
        }    
           
    }
}