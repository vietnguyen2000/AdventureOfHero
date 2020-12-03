using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    // Start is called before the first frame update
    protected int movementInputDirection;

    protected bool isWalking;
    protected bool isFacingRight = true;
    protected bool isGrounded;
    protected bool Jumping;
    protected bool Falling;
    protected float KnockBackDelay;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected GameObject CheckSurround;
    protected CapsuleCollider2D Collider;

    public float jumpForce;

    private BoxCollider2D GroundCheckTrigger;
    private float lastKeyDown;
    private KeyCode lastKeyCode;
    private bool Sliding;

    private bool isSitting;
    private bool checkKey;
    public float Jump2Height;
    public bool endAnimAttack;
    public bool isDead;

    public LayerMask groundLayer;
    protected void SetValueDefault()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Collider = GetComponent<CapsuleCollider2D>();
        jumpForce = 23f;
        groundLayer = 1<< LayerMask.NameToLayer("Ground");

        CreateCheckSurround();
    }
    void Start()
    {
        SetValueDefault();
        Jump2Height = 0.85f;
        Time.timeScale = 1f;
        // StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateAnimationsPlayer();
        
    }
    private void CreateCheckSurround() {
        CheckSurround = new GameObject("CheckSurround");
        CheckSurround.AddComponent<BoxCollider2D>();
        GroundCheckTrigger = CheckSurround.GetComponent<BoxCollider2D>();
        GroundCheckTrigger.isTrigger = true;
        GroundCheckTrigger.size = new Vector2(Collider.size.x, 0.25f);
        GameObject clone = Instantiate(CheckSurround, gameObject.transform);
        GroundCheckTrigger = clone.GetComponent<BoxCollider2D>();
        Object.Destroy(CheckSurround);
        CheckSurround = clone;
    }
    private void FixedUpdate()
    {
        CheckAnimate();
        UpdateAnimations();
        CheckingSurround();
    }

    protected void CheckAnimate()
    {
        isWalking = isGrounded;
        if (isGrounded)
        {
            Jumping = false;
            Falling = false;
        }

        else if (rb.velocity.y < 0.0001f && isSitting == false) Falling = true;

        if (rb.velocity.y > 0.001f) Falling = false;

        if (Falling == true || Jumping == true)
        {
            isSitting = false;
        }
    }
    private void UpdateAnimationsPlayer()
    {
        anim.SetBool("isSitting", isSitting);
    }

    protected void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("Falling", Falling);
        anim.SetBool("Sliding", Sliding);
        

    }
    private void CheckInput()
    {
        movementInputDirection = 1;
        if (Input.GetKeyDown(OptionsScript.keys["Jump"]))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
        if (Input.GetKeyUp(OptionsScript.keys["Jump"]))
        {
            if(rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
        }

        if (Input.GetKey(OptionsScript.keys["SitDown"]))
        {
            if (!Falling && isGrounded)  {
                Sliding = true;
                
            }
        }
        if (Input.GetKeyUp(OptionsScript.keys["SitDown"]) || Input.GetKeyUp(OptionsScript.keys["SitDown"]))
        {
            if (Sliding) Sliding = false;
        }
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckingSurround()
    {
        isWalking = isGrounded = GroundCheckTrigger.IsTouchingLayers(groundLayer);
    }

    // Kiểm tra nếu main đụng trúng bẫy hoặc quái thì kết thúc trò chơi.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Collider.IsTouching(collision))
        {
            if (collision.gameObject.layer == 12 || collision.gameObject.layer == 13) //Trap or Monster
            {
                isDead = true;
                Destroy(gameObject);    // Giết main.
                Time.timeScale = 0;
            }
        }
    }
}
