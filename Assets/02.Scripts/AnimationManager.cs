using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Animator playeranimator;
    [SerializeField]
    Rigidbody2D playerrigidbody2D;
    [SerializeField]
    csPlayer player;

    float rollTime = -3f;

    void Update()
    {
        JumpAnim();
        MoveAnim();
        RollAnim();
    }

    void MoveAnim()
    {
        if (Input.GetButton("Horizontal"))
            playeranimator.SetBool("moving", true);
        else
            playeranimator.SetBool("moving", false);
    }

    void JumpAnim()
    {
        playeranimator.SetFloat("velocityY", playerrigidbody2D.velocity.y);
        
        if (Input.GetButtonDown("Jump") && player.IsJump)
        {
            playeranimator.SetTrigger("Jump");
            
            playeranimator.SetBool("jumping", true);
        }

        //점프 모션 종료 : 착지 모션 시작
        else if (player.IsJump)//&& playerrigidbody2D.velocity.y <= 0)
        {
            playeranimator.SetBool("jumping", false);
        }
    }

    void RollAnim()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.IsJump && Time.time - rollTime > 3.0f )
        {
            rollTime = Time.time;
            playeranimator.SetTrigger("Roll");
        }
    }
}
