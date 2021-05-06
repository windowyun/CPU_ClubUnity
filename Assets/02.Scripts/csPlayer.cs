using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayer : MonoBehaviour
{
    public float speed = 20.0f;
    public float jumpPower = 20.0f;

    bool isGround;
    public float checkJump;
    public Transform pos;

    Rigidbody2D rigid = new Rigidbody2D();
    Transform trans;
    SpriteRenderer render;
    Animator anim;
    [SerializeField]
    LayerMask islayer;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        /*
        Vector2 postion = transform.position;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Input.GetButton("Horizontal"))
        {
            render.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if (Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);
        */
        
        if (Input.GetAxis("Horizontal") > 0)
            render.flipX = false;
        else if(Input.GetAxis("Horizontal") < 0)
            render.flipX = true;

        if (Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        isGround = Physics2D.OverlapCircle(trans.position, checkJump, islayer);
        if (Input.GetButtonDown("Jump") && isGround == true)
        {
            //rigid.velocity = Vector2.up * jumpPower;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        
        //Postion 직접 변경
        //postion.x += h * speed * Time.deltaTime;
        //trans.position = postion;

        //TransLate
        //trans.Translate(new Vector3(h * speed * Time.deltaTime, 0, 0));
    }
    
    void FixedUpdate()
    {
        //rigidBody2D.velocity 이동 velocity : 리지드 바디의 현재 속도
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        /*
        if(Input.GetButtonDown("Jump"))
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
        */
        /*
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if(Mathf.Abs(rigid.velocity.x) > 3)
        {
            rigid.velocity = new Vector2(3.0f * h, rigid.velocity.y);
        }
        */
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
    }
    
}
