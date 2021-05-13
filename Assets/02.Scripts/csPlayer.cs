using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayer : MonoBehaviour
{
    [Header("Move & Jump")]
    public float speed = 20.0f;
    public float jumpPower = 20.0f;
    [Header("BoxCast")]
    [SerializeField] Vector2 boxCastSize = new Vector2(0.28f, 0.01f);
    [SerializeField] float boxCastDistance = 0.03f;
    [SerializeField] Vector3 boxCastStart = new Vector3(0.0f, -1.1f, 0.0f);
    [Header("OverlapCircle")]
    [SerializeField] Vector3 overlapCircleStart = new Vector3(0.0f, -1.1f, 0.0f);
    [SerializeField] float overlapCircleRadius = 0.03f;
    
    float timer;
    bool isJump = false;
    bool isGround;
    
    Rigidbody2D rigid = new Rigidbody2D();
    Transform trans;
    SpriteRenderer render;
    Animator anim;
    CapsuleCollider2D coli;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coli = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        anim.SetFloat("velocityY", rigid.velocity.y);
        isJump = Physics2D.OverlapCircle(trans.position + overlapCircleStart, overlapCircleRadius, LayerMask.GetMask("Ground"));
        
        /*
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(trans.position + boxCastStart, boxCastSize, 0f, Vector2.down, boxCastDistance, LayerMask.GetMask("Ground"));
        if (raycastHit2D.collider != null)
        {
            isJump = false;
            anim.SetBool("jumping", false);
        }
        */

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
        else if (Input.GetAxis("Horizontal") < 0)
            render.flipX = true;

        if (Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        //isGround = Physics2D.OverlapCircle(trans.position, checkJump, islayer);
        if (Input.GetButtonDown("Jump") && isJump)// && isGround == true)
        {
            //isJump = true;
            
            anim.SetTrigger("Jump");
            //rigid.velocity = Vector2.up * jumpPower;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
            
        }

        if(isJump && rigid.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("laying", true);
            coli.offset = new Vector2(0, -0.25f);
            coli.size = new Vector2(0.13f, 0.25f);
        }
        else
        {
            anim.SetBool("laying", false);
            coli.offset = new Vector2(0, -0.21f);
            coli.size = new Vector2(0.13f, 0.33f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack");
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
        if(isJump)
        {
            anim.SetTrigger("Jump");
            //rigid.velocity = Vector2.up * jumpPower;
            rigid.AddForce(Vector2.up * jumpPower);//, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
        }
        */
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


    /*
    // OncpllisionEnter 으로 땅 확인
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
            anim.SetBool("jumping", false);
        }
    }
    */

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + overlapCircleStart, overlapCircleRadius);
        /*
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position + boxCastStart, boxCastSize, 0f, Vector2.down, boxCastDistance, LayerMask.GetMask("Ground"));

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position + boxCastStart, Vector2.down * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + boxCastStart + Vector3.down * raycastHit.distance , boxCastSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position + boxCastStart, Vector2.down * boxCastDistance);
        }
        */
    }
}