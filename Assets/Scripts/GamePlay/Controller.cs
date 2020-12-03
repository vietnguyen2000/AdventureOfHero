using UnityEngine;

public class Controller : MonoBehaviour
{
    public float startHP = 5f;
    public float Damage = 1f;
    public int Score;
    public float movementSpeed = 5f;
    public float jumpForce = 18f;
    public float KnockBackDelay = 0.8f;
    public float hurtDelayTime = 0;

    protected bool isWalking;
    protected bool isFacingRight = true;
    protected bool isGrounded;
    protected bool Jumping;
    protected bool Falling;
    protected bool isTouchingWall;
    protected bool canMove;
    protected bool canFlip = true;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected GameObject CheckSurround;
    protected CapsuleCollider2D Collider;

    protected float movementInputDirection;
    protected float wallCheckDistance;
    protected float AttackDelay;
    protected float m_KnockBackDelay;
    protected float m_movementSpeed;

    protected PowerValue powerValue;
    protected LayerMask groundLayer;


    private Material whiteFlash;
    private Material matDefault;
    private float hurtDelay;
    

    private bool die;

    protected virtual void Awake()
    {
        // add and get component for controller
        powerValue = gameObject.AddComponent<PowerValue>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Collider = GetComponent<CapsuleCollider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");                                      //set the ground layer to check ground
        whiteFlash = Resources.Load<Material>("Effect/Hurt/WhiteFlash");                         //matierial flash for effect white flashing
        matDefault = gameObject.GetComponent<SpriteRenderer>().material;
        powerValue.startHP = startHP;
        powerValue.basicDamage = Damage;
        powerValue.Score = Score;
        rb.gravityScale = 5.8f;                                                                  // make sure the weight of object
        wallCheckDistance = Collider.size.x / 2;                                         //Distance to check touching wall
        m_movementSpeed = movementSpeed;
    }
    protected virtual void Start()
    {
        GameObject temp = new GameObject("CheckSurround");
        CheckSurround = Instantiate(temp, gameObject.transform);      //Create the check surround: check wall and check ground
        Object.Destroy(temp);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        die=anim.GetBool("Die");
        if (!die)
        {
            CheckingSurround();
            CheckParamettersOfAnimator();
            UpdateAnimations();

            CheckMovementDirection();
            Action();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    protected virtual  void Action()
    {
        // move left or right
        if(m_KnockBackDelay <=0f)  rb.velocity = new Vector2(movementInputDirection * m_movementSpeed, rb.velocity.y);

        //check for active KnockBack
        ActiveKnockBack(); 
    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection == -1)
        {
            Flip();
        }
        if (!isFacingRight && movementInputDirection == 1)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    protected virtual void CheckParamettersOfAnimator()
    {
        if (movementInputDirection != 0 && rb.velocity.x!=0) isWalking = true;
        else isWalking = false;
        if (rb.velocity.y < 0f && !isGrounded) Falling = true;
        if (rb.velocity.y >= 0f) Falling = false;
    }
    protected virtual void UpdateAnimations()
    {  
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("Falling", Falling);
        anim.SetBool("isTouchingWall", isTouchingWall);
        anim.SetFloat("AttackDelay", AttackDelay);
        anim.SetFloat("KnockBackDelay", m_KnockBackDelay);
        anim.SetFloat("HurtDelay", hurtDelay);

        if (isGrounded)
        {
            if (AttackDelay > 0f)
            {
                anim.SetBool("Attack1", true);
            }
            else
            {
                anim.SetBool("Attack1", false);
            }
        }
        else anim.SetBool("Attack1", false);
    }

    protected virtual bool BeHurted(GameObject reason)
    {
        if (hurtDelay <= 0f)
        {
            PowerValue reasonPowerValue = reason.GetComponentInParent<PowerValue>();
            powerValue.GetDamage(reasonPowerValue.damage);                                  //get Damage for the knock length
            bool isRightSide;
            if (reason.transform.position.x >= rb.position.x) isRightSide = true;
            else isRightSide = false;                                                       //Check position of reason damage 

            if (isRightSide) rb.velocity = new Vector2(Mathf.Max(-4f * reasonPowerValue.damage/1.5f,-8f), 4f);
            else rb.velocity = new Vector2(Mathf.Min(4f * reasonPowerValue.damage/1.5f,8f), 4f);

            AttackDelay = 0f;
            m_KnockBackDelay = KnockBackDelay;                                                          //Ative Knock Back
            hurtDelay = hurtDelayTime;                                                        //Active hurt Delay
            MakeEffectWhileFlash();                                                           //Make Effect White Flash
            return true;
        }
        return false;
    }
    private void MakeEffectWhileFlash()
    {
        gameObject.GetComponent<SpriteRenderer>().material = whiteFlash;
        Invoke("ResetMaterial", 0.2f);
    }
    private void ResetMaterial()
    {
        gameObject.GetComponent<SpriteRenderer>().material = matDefault;
    }
    private void ActiveKnockBack()
    {

        if (m_KnockBackDelay >= 0)                                          //Start knock back
        {
            m_KnockBackDelay -= Time.deltaTime;
            if (rb.velocity.x < -0.5 || rb.velocity.x > 0.5)                //add force to stop Knock
            {
                if (rb.velocity.x>0) rb.AddForce(new Vector2(-20f, 0f));
                else rb.AddForce(new Vector2(20f, 0f));
            }
            else                                                            //Stop Knock
            {
                m_KnockBackDelay = -0.001f;
            }
        }
        else                                                                //Start countdown hurt delay (invisible)
        {
            if (hurtDelay >= 0f) hurtDelay -= Time.deltaTime;
        }
    }
    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void CheckingSurround()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(CheckSurround.transform.position.x, CheckSurround.transform.position.y), new Vector2(Collider.size.x * Mathf.Abs(transform.localScale.x) - 0.2f, 0.25f*transform.localScale.y), 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(new Vector2(CheckSurround.transform.position.x + (Collider.offset.x + wallCheckDistance)*transform.localScale.x, CheckSurround.transform.position.y + Collider.offset.y), new Vector2(wallCheckDistance, Collider.size.y*50/100), 0, groundLayer);
    }
    //protected virtual void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(new Vector3(CheckSurround.transform.position.x + (Collider.offset.x + wallCheckDistance) * transform.localScale.x, CheckSurround.transform.position.y + Collider.offset.y, 0), new Vector3(wallCheckDistance, Collider.size.y * 50 / 100, 0));
    //    Gizmos.DrawWireCube(new Vector3(CheckSurround.transform.position.x, CheckSurround.transform.position.y, 0), new Vector3(Collider.size.x * Mathf.Abs(transform.localScale.x) - 0.2f, 0.25f * transform.localScale.y, 0));
    //}
}
