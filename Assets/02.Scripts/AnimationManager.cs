using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Animator playeranimator1;
    [SerializeField]
    Animator playeranimator2;
    [SerializeField]
    Rigidbody2D playerrigidbody2D1;
    [SerializeField]
    Rigidbody2D playerrigidbody2D2;
    [SerializeField]
    csPlayer player1;
    [SerializeField]
    csPlayer player2;
    [SerializeField]
    ChangePlayer changePlayer;

    Animator currentAnimator;
    Rigidbody2D currentRigid2D;
    csPlayer player;

    float rollTime = -3f;

    void Awake()
    {
        ChangePlay();
    }

    void Update()
    {
        JumpAnim();
        MoveAnim();
        RollAnim();

        ChangePlay();
    }

    void ChangePlay()
    {
        if (changePlayer.Currentstats)
        {
            currentAnimator = playeranimator1;
            currentRigid2D = playerrigidbody2D1;
            player = player1;
        }

        else
        {
            currentAnimator = playeranimator2;
            currentRigid2D = playerrigidbody2D2;
            player = player2;
        }
    }

    void MoveAnim()
    {
        if (Input.GetButton("Horizontal"))
            currentAnimator.SetBool("moving", true);
        else
            currentAnimator.SetBool("moving", false);
    }

    void JumpAnim()
    {
        currentAnimator.SetFloat("velocityY", currentRigid2D.velocity.y);

        if (Input.GetButtonDown("Jump") && player.IsJump)
        {
            Debug.Log("asd");
            currentAnimator.SetTrigger("Jump");
            
            currentAnimator.SetBool("jumping", true);
        }
        
        //점프 모션 종료 : 착지 모션 시작
        else if (player.IsJump)//&& playerrigidbody2D.velocity.y <= 0)
        {
            currentAnimator.SetBool("jumping", false);
        }

    }

    void RollAnim()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.IsJump && Time.time - rollTime > 3.0f )
        {
            rollTime = Time.time;
            currentAnimator.SetTrigger("Roll");
        }
    }
}
