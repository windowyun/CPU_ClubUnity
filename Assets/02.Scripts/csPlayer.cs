using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayer : MonoBehaviour
{
    [Header("Move & Jump & Roll")]
    public float speed = 3.0f;
    public float rollSpeed = 30.0f;
    public float jumpPower = 4.0f;
    /*
    [Header("BoxCast")]
    [SerializeField] Vector2 boxCastSize = new Vector2(0.28f, 0.01f);
    [SerializeField] float boxCastDistance = 0.03f;
    [SerializeField] Vector3 boxCastStart = new Vector3(0.0f, -1.1f, 0.0f);
    */
    [Header("OverlapCircle")]
    [SerializeField] Vector3 overlapCircleStart = new Vector3(0.0f, -1.1f, 0.0f);
    [SerializeField] float overlapCircleRadius = 0.03f;
    [Header("CoolTime")]
    public float rollCoolTime = 0f;
    public float attackCoolTime = 0f;

    public float rollTime = 0f;
    public float attackTime = 0f;

    float timer;
    bool isJump = false;
    bool isGround;
    bool isRoll;

    //GameManager gameManager = new GameManager();

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
        rollTime += Time.deltaTime;
        attackTime += Time.deltaTime;

        if (rollTime > 100)
            rollTime = rollCoolTime;

        if (attackTime > 100)
            attackTime = attackCoolTime;

        anim.SetFloat("velocityY", rigid.velocity.y);
        // OverlapCircle로 땅 확인
        isJump = Physics2D.OverlapCircle(trans.position + overlapCircleStart, overlapCircleRadius, LayerMask.GetMask("Ground"));

        if (Input.GetAxis("Horizontal") > 0 && !isRoll)
            render.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0 && !isRoll)
            render.flipX = true;

        if (Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        //isGround = Physics2D.OverlapCircle(trans.position, checkJump, islayer);
        if (Input.GetButtonDown("Jump") && isJump && !isRoll)// && isGround == true)
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

        if (Input.GetKey(KeyCode.DownArrow) && rigid.velocity == Vector2.zero && !isRoll)
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

        if (Input.GetKeyDown(KeyCode.Z) && !isRoll && attackTime >= attackCoolTime)
        {
            attackTime = 0f;
            anim.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && rigid.velocity.y == 0f && isJump && rollTime >= rollCoolTime)
        {
            rollTime = 0f;
            anim.SetTrigger("Roll");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skull2Roll"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                isRoll = false;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                isRoll = true;
            }
        }

    }

    void FixedUpdate()
    {
        //rigidBody2D.velocity 이동 velocity : 리지드 바디의 현재 속도
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float hf = Input.GetAxisRaw("Horzontal");

        if(isRoll)
        {
            if (render.flipX == false)
                rigid.velocity = new Vector2(1f * rollSpeed , rigid.velocity.y);
            else if (render.flipX == true)
                rigid.velocity = new Vector2(-1f * rollSpeed , rigid.velocity.y);
        }

        else
            rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
        //Debug.Log(rigid.velocity.x);
    }

    void Laying()
    {
        if (Input.GetKey(KeyCode.DownArrow) && rigid.velocity == Vector2.zero)
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
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move 모션
        if (Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        //Postion 직접 변경
        //postion.x += h * speed * Time.deltaTime;
        //trans.position = postion;

        //TransLate
        //trans.Translate(new Vector3(h * speed * Time.deltaTime, 0, 0));

        //rigidBody2D.velocity 이동 velocity : 리지드 바디의 현재 속도
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
    }

    void Jump()
    {

        // OverlapCircle로 땅 확인
        isJump = Physics2D.OverlapCircle(trans.position + overlapCircleStart, overlapCircleRadius, LayerMask.GetMask("Ground"));

        /*
        // boxcast로 땅 확인
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(trans.position + boxCastStart, boxCastSize, 0f, Vector2.down, boxCastDistance, LayerMask.GetMask("Ground"));
        if (raycastHit2D.collider != null)
        {
            isJump = false;
            anim.SetBool("jumping", false);
        }
        */

        if (Input.GetButtonDown("Jump") && isJump)// && isGround == true)
        {
            //isJump = true;

            anim.SetTrigger("Jump");
            
            //velocity 점프
            //rigid.velocity = Vector2.up * jumpPower;
            //최고 속도 제한
            /*
            if (Mathf.Abs(rigid.velocity.x) > 3)
            {
                rigid.velocity = new Vector2(3.0f * h, rigid.velocity.y);
            }
            */

            //AddForce 점프
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);

        }

        //점프 모션 종료 : 착지 모션 시작
        if (isJump && rigid.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
        }
    }

    void FilpX() //방향 전환
    {
        
        /*
        if (Input.GetButton("Horizontal"))
        {
            render.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        */

        if (Input.GetAxis("Horizontal") > 0)
            render.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            render.flipX = true;
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