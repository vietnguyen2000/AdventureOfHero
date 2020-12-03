using UnityEngine;
using System.IO;
using System.Collections;

public class PlayerController : Controller
{
    public float slideSpeed;
    public float slideLength;
    public float wallSlideSpeed;
    public float Jump2Height;


    private int Attacking;
    private float Slide;
    private float lastKeyDown;
    private KeyCode lastKeyCode;

    private bool DoubleJump;
    private bool isSitting;
    private bool isWallSliding;
    private bool WallJumping = false ;
    private bool checkKey;
    private bool isDefensing;
    private bool doubleRun;

    private GameObject DoubleRunEffect;
    private GameObject SmokeLanding;
    private GameObject Block;
    GameObject GameOver;

    [HideInInspector]
    public bool startAnimAttack, endAnimAttack;
    bool isPlayedSoundAttack;
    [HideInInspector]
    public bool FireSp, IceSp, WindSp, GroundSp;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //load data player
        LoadData();
        powerValue.MaxShield = 3;

        //Load effect
        DoubleRunEffect = (GameObject)Resources.Load("Effect/DoubleRunEffectAnim");
        SmokeLanding = (GameObject)Resources.Load("Effect/SmokeLanding");
        Block = (GameObject)Resources.Load("Effect/Hit/Block");
        GameOver = GameObject.Find("GameOverMenu");

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (m_KnockBackDelay <= 0 && Time.timeScale != 0) CheckInput();
        if (powerValue.HP <= 0 )
        {
            Transform camera = GameObject.Find("Main Camera").GetComponent<Transform>();
            if (Vector3.Distance(camera.position, GameOver.transform.position) >= 2f)
            GameOver.transform.position = GameOver.transform.position + (camera.position - GameOver.transform.position) * Time.deltaTime * 3f;
        }  
    }
    protected override void Action()
    {
        base.Action();
        if (m_KnockBackDelay <= 0f)  //if not knockback
        {
            if (Slide <= 0f && AttackDelay <= 0f && !WallJumping && !isDefensing) rb.velocity = new Vector2(movementInputDirection * m_movementSpeed, rb.velocity.y);
            //just move left or right when not: slide, attack and defene

            if (Slide > 0f) Sliding();                              // active sliding
            WallSlide();                                            //active WallSlide
            Attack();                                               //active Attack
            CheckCanFlip();
        }
        else                    //if KnockBack is active
        {
            Slide = 0;          //stop sliding
        }
    }
    private void CheckCanFlip()
    {
        if (Slide <= 0 && (AttackDelay <= 0 || endAnimAttack))  //if when sliding and attacking canflip is false
        {
            canFlip = true;
        }
        else canFlip = false;
    }
    protected override void CheckParamettersOfAnimator()
    {
        if (movementInputDirection != 0 && rb.velocity.x != 0 && !isTouchingWall) isWalking = true;
        else isWalking = false;
        if (isGrounded)                    //when grounded reset all value jump
        {
            Jumping = false;
            Falling = false;
            DoubleJump = false;
            WallJumping = false;
        }

        else                                //when not grounded, falling just true when veloicty.y <0
        {
            if (rb.velocity.y < 0f) Falling = true;
            if (rb.velocity.y >= 0f) Falling = false;
        }

        if (Falling == true || isWalking == true || Jumping == true)
        {
            isSitting = false;              //Can not sit when fall, walk, jump
        }
        if (isWallSliding)
        {
            WallJumping = false;            //when slide on wall, WallJump is enable
        }
        
    }
    protected override void UpdateAnimations()
    {
        base.UpdateAnimations();
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallJumping", WallJumping);
        anim.SetBool("isSitting", isSitting);
        anim.SetFloat("Slide", Slide);
        anim.SetBool("DoubleJump", DoubleJump);
        anim.SetBool("isDefensing", isDefensing);
        anim.SetBool("doubleRun", doubleRun);
        anim.SetFloat("HP", powerValue.HP);

        //animator for attacking, and set the damage for each attack
        if (isGrounded)                        //attack on ground
        {
            if (AttackDelay > 0f)               //if attack is active
            {
                switch (Attacking)              //count for attacking
                {
                    case 1:
                        anim.SetBool("Attack1", true);
                        break;
                    case 2:
                        anim.SetBool("Attack2", true);
                        powerValue.damage = powerValue.basicDamage * 1.5f;
                        break;
                    case 3:
                        anim.SetBool("Attack3", true);
                        powerValue.damage = powerValue.basicDamage * 2f;
                        break;
                }
            }
            else                                 //when attack delay is time out
            {
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
                anim.SetBool("Attack3", false);
                powerValue.damage = powerValue.basicDamage;
            }
        }
        else                                     //Attack when flying
        {
            if (AttackDelay > 0f)
            {
                switch (Attacking)
                {
                    case 1:
                        anim.SetBool("Attack1", true);
                        break;
                    case 2:
                        anim.SetBool("Attack2", true);
                        powerValue.damage = powerValue.basicDamage * 1.5f;
                        break;
                    case 3:
                        anim.SetBool("Attack3", true);
                        powerValue.damage = powerValue.basicDamage * 2f;
                        break;
                }
            }
            if(isWallSliding==true)                //Attack when fly just reset why slide wall, or grounded
            {
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
                anim.SetBool("Attack3", false);
                powerValue.damage = powerValue.basicDamage;
            }
        }
    }

    private void CheckInput()                           //Check the KeyBoard input
    {
        if (!WallJumping && Slide <= 0f)
        {
            if (Input.GetKey(OptionsScript.keys["Left"]) ^ Input.GetKey(OptionsScript.keys["Right"]))
            {
                if (Input.GetKey(OptionsScript.keys["Left"]))
                {
                    movementInputDirection = -1;
                }
                else movementInputDirection = 1;
                //SFXManager.PlaySoundEffectOneShot(SFXManager.Run, 0.5f);
                //SFXManager.PlaySoundEffectOneShot(SFXManager.Run[1], 0.4f,0.2f);

            }
            else movementInputDirection = 0;
            
        }//if 'A' pressing, return -1, else if 'D' pressing return 1, else return 0

        CheckDoubleRun();                                               //Check for double key A or D press to active double run.

        if (Input.GetKeyDown(OptionsScript.keys["Jump"]))
        {                                                               //Key SPACE
            if (isGrounded)
            {                                                           //When grounded
                if (AttackDelay <= 0)
                {
                    Jump();                                             //Jump when not attacking and make effect smoke
                    if (doubleRun) MakeEffect(SmokeLanding);
                }

            }
            else
            {                                                           //if not grounded
                if (isWallSliding)
                {
                    WallJumping = true;
                    WallJump();                                         //WallJump when sliding wall
                }
                else if (!DoubleJump)
                {
                    DJump();                                            //Double jump if double jump is enable
                }

            }
        }
        if (Input.GetKeyUp(OptionsScript.keys["Jump"]))
        {
            if(rb.velocity.y>0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f); //Prevent the jump when keyUp SPACE
        }

        if (Input.GetKey(OptionsScript.keys["SitDown"]))                                        //Key S
        {
            if (Falling == false && isWalking == false && Jumping == false)  isSitting = true;  //Active sitting when not falling walking and jumping
        }
        if (Input.GetKeyUp(OptionsScript.keys["SitDown"]))
        {
            isSitting = false;                                                                  
        }

        if (Input.GetKeyDown(OptionsScript.keys["Slide"]))                                    //Key K
        {
            if (Slide < 0.01f && isGrounded && AttackDelay <= 0f)
            {
                Slide = slideLength;                                        //Slide when grounded and not attacking
                MakeEffect(DoubleRunEffect);
            }
        }

        if (Input.GetKey(OptionsScript.keys["Defense"]))                                        //Key J
        {
            if (isGrounded && AttackDelay <= 0f && Slide <= 0f)
            {   
                isDefensing = true;                                         //Active isDefensing
                rb.velocity = new Vector2(0, 0);                            //and Stop moving
            }
        }
        if (Input.GetKeyUp(OptionsScript.keys["Defense"]))
        {
            if (isDefensing == true) isDefensing = false;
        }

        if (Input.GetKeyDown(OptionsScript.keys["Attack"]))                                    //Key H for attacking
        {
            if (Slide<=0 && !isWallSliding && m_KnockBackDelay <=0f)          //just attack when not slide, not wall slide and not knock
            {
                if (!isGrounded)                                            //On flying
                {
                    if (AttackDelay < 0.15f)                                //Just Do Attack when not attack, or last attack is near finish
                    {
                        if (Attacking <= 2)
                        {
                            Invoke("comeToNextAttack", AttackDelay - 0.1f);                                    //count for attacking
                            if (Attacking == 1) AttackDelay += 0.35f;       //Adding time to active attack
                            else AttackDelay += 0.25f;                      //Attack 1 is spend more time than other
                            rb.velocity = new Vector2(rb.velocity.x*0.6f, 21.5f * 0.8f * 0.8f); //make the 'flappy'
                           
                        }
                    }
                }
                else                                                        //On Ground
                {
                    if (AttackDelay < 0.4f)                                 //Just Do Attack when not attack, or last attack is near finish
                    {
                        if (Attacking <= 2)
                        {
                            Invoke("comeToNextAttack", AttackDelay-0.1f);                      //count for attacking
                            AttackDelay += 0.5f;                            //Adding time to active attack
                        }
                    }
                }
            }
        }
    }
    void comeToNextAttack()
    {
        Attacking++;
    }
    private void Sliding()
    {
        if (Slide > 0)                          //just slide when Slide is active
        {
            Slide -= Time.deltaTime;
            int direction;
            if (isFacingRight == true) direction = 1;
            else direction = -1;                                            //check directon of face
            rb.velocity = new Vector2(direction*slideSpeed, rb.velocity.y); //slide to front 
        }
    }
    private void DJump()
    {
        if (AttackDelay <= 0f && !DoubleJump)                       // just double jump when enable and not attacking                              
        {
            DoubleJump = true;

            if (!WallJumping) rb.velocity = new Vector2(rb.velocity.x, jumpForce * Jump2Height);  //normal double jump
            else if (WallJumping && rb.velocity.y < 1.0f)                                         //double jump after wall Jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.6f);                       //DoubleJumpHeight is lower
                WallJumping = false;                                                              
            }
            else DoubleJump = false;                                                              //Make sure, alway after walljump, you can double jump
        }
    }

    private void WallJump()
    {
        isWallSliding = false;                                                                    //Make sure this frame is not wallslideing for the animation so smooth
        Flip();                                                                                   
        if (isFacingRight)
        {
            movementInputDirection = 1;
        }
        else movementInputDirection = -1;
        rb.velocity = new Vector2(movementInputDirection * 10f, jumpForce*0.8f);                               //boost speed
        DoubleJump = false;
    }
    private void WallSlide()
    {
        if (movementInputDirection != 0) checkKey = true;
        else checkKey = false;                                                                      //Check key A or D is pressed
        if (!isGrounded && isTouchingWall && rb.velocity.y <= 0f && checkKey)                       //just slide on wall when key A,D press, and touching wall with falling
        {
            isWallSliding = true;                                                                   //Actve wallslide
            AttackDelay = 0f;
            Attacking = 0;                                                                          //reset attacking
        }
        else isWallSliding = false;

        //When wall slide active
        if (isWallSliding)
        {
            rb.velocity = new Vector2(0f, -wallSlideSpeed);
        }
    }

    private void Attack()
    {
        if (!isGrounded)
        {
            if (AttackDelay > 0f)                   //When attack is active
            {
                if (Attacking <= 2)
                {
                    AttackDelay -= Time.deltaTime;
                }
                else
                {
                    rb.velocity = new Vector2(0, -20f); //Attack 3 when fly 
                }
            }
        }
        else
        {
            if (AttackDelay > 0f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                AttackDelay -= Time.deltaTime;

            }
            else
            {
                Attacking = 0;
            }
        }
    }
    private void CheckDoubleRun()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(OptionsScript.keys["Left"]) || Input.GetKeyDown(OptionsScript.keys["Right"]))
            {
                float curKeyDown = Time.time; //time for this Key down
                if (lastKeyCode == OptionsScript.keys["Left"] && Input.GetKeyDown(OptionsScript.keys["Left"]) || lastKeyCode == OptionsScript.keys["Right"] && Input.GetKeyDown(OptionsScript.keys["Right"]))
                { //when last key code = this key code

                    if (curKeyDown - lastKeyDown <= 0.18f)
                    { // and delta time < 0.18f
                        m_movementSpeed = movementSpeed*1.5f;
                        MakeEffect(DoubleRunEffect);
                        doubleRun = true;
                    }
                }
                else
                {
                    if (movementInputDirection == 1) lastKeyCode = OptionsScript.keys["Right"];
                    else if (movementInputDirection == -1) lastKeyCode = OptionsScript.keys["Left"];
                }
                lastKeyDown = curKeyDown;
                //set last key
            }
        }

        //check for Cancel double run
        if (Input.GetKeyUp(OptionsScript.keys["Left"]) || Input.GetKeyUp(OptionsScript.keys["Right"]))
        {
            if (movementInputDirection == 0) //cancel when no key is press
            {
                m_movementSpeed = movementSpeed;
                doubleRun = false;
            }
        }
        if (AttackDelay > 0 || isDefensing || isWallSliding) // or when attack, defense, wallslide is active
        {
            m_movementSpeed = movementSpeed;
            doubleRun = false; 
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == true) //if collision is trigger
        {
            if (collision.gameObject.layer == 12) //layer 12 is trap
            {
                BeHurted(collision.gameObject); //get hurt
            }
            if (collision.tag == "Gate")
            {
                if (Input.GetKeyDown(OptionsScript.keys["Up"]))
                {
                    MoveNewSence gate;
                    if (collision.gameObject.TryGetComponent<MoveNewSence>(out gate))
                    {
                        SaveData();
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == true)
        {
            if (collision.gameObject.layer == 16)
            {
                if ((isFacingRight && collision.transform.lossyScale.x < 0 || !isFacingRight && collision.transform.lossyScale.x >= 0) && isDefensing)
                {
                    //make effect when defense success
                    MakeEffect(Block, new Vector3(transform.position.x + 0.45f * transform.localScale.x, transform.position.y + 1.35f, 0));
                    SFXManager.PlaySoundEffectOneShot(SFXManager.Defense);
                    powerValue.lastTimeGetDame = Time.time;
                    
                }
                else
                {
                    BeHurted(collision.gameObject); //if defense is fault, get hurt
                }
                
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            //ignore the collider of monster
            Physics2D.IgnoreCollision(Collider, collision.collider);
        }
        if (collision.gameObject.layer == 9) //layer 9 is ground
        {
            //make effect smoke when enter the ground
            if (Falling && !isTouchingWall)
            {
                MakeEffect(SmokeLanding);
                SFXManager.PlaySoundEffectOneShot(SFXManager.Stand);
            }
        }
    }
    public PlayerData CreatePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.movementSpeed = movementSpeed;
        playerData.jumpForce = jumpForce;
        playerData.HP = powerValue.HP;
        playerData.BasicDamage = powerValue.basicDamage;
        playerData.Score = powerValue.Score;
        playerData.FireSp = FireSp;
        playerData.IceSp = IceSp;
        playerData.WindSp = WindSp;
        playerData.GroundSp = GroundSp;
        return playerData;
    }
    public void SaveData()
    {
        PlayerData playerData = CreatePlayerData();
        string json = JsonUtility.ToJson(playerData);
        string path = Application.dataPath + "/SavePlayerData.txt";
        File.WriteAllText(path, json);
        Debug.Log("Save Success!!!");
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    public void LoadData()
    {
        string json;
        json = File.ReadAllText(Application.dataPath + "/SavePlayerData.txt");
        PlayerData playerData = new PlayerData();
        //if (temp == null)
        //{
        //    json = JsonUtility.ToJson(PlayerData);
        //    string path = Application.dataPath + "/Resources/Files/SavePlayerData.txt";
        //    File.WriteAllText(path, json);
        //}
        //else json = temp.text;
        playerData = JsonUtility.FromJson<PlayerData>(json);
        movementSpeed = playerData.movementSpeed;
        jumpForce = playerData.jumpForce;
        powerValue.HP = playerData.HP;
        powerValue.basicDamage = playerData.BasicDamage;
        powerValue.Score = playerData.Score;
        if (playerData.FireSp)
        {
            //Debug.Log("asda");
            GameObject Support = Resources.Load<GameObject>("GameObject/Fire");
            StartCoroutine(InstantiateDelay(0.2f, Support));
            FireSp = true;
        }
        if (playerData.IceSp)
        {
            GameObject Support = Resources.Load<GameObject>("GameObject/Ice");
            StartCoroutine(InstantiateDelay(0.2f, Support));
            IceSp = true;
        }
        if (playerData.WindSp)
        {
            GameObject Support = Resources.Load<GameObject>("GameObject/Wind");
            StartCoroutine(InstantiateDelay(0.2f, Support));
            WindSp = true;
        }
        if (playerData.GroundSp)
        {
            GameObject Support = Resources.Load<GameObject>("GameObject/Ground");
            StartCoroutine(InstantiateDelay(0.2f, Support));
            GroundSp = true;
        }
        Debug.Log("Load Success!!!");
//#if UNITY_EDITOR
//        UnityEditor.AssetDatabase.Refresh();
//#endif
    }
    private IEnumerator InstantiateDelay(float seconds, GameObject Support)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("instantiate support");
        InstantiateSupport(Support);
    }
   private void InstantiateSupport(GameObject Support)
    {
        Instantiate(Support, transform.position, new Quaternion());
    }
    private void MakeEffect(GameObject Effect)
    {
        GameObject effect = Instantiate(Effect, transform.position, new Quaternion());
        effect.transform.localScale = transform.localScale;
        Object.Destroy(effect, 2f);
    }
    private void MakeEffect(GameObject Effect,Vector3 position)
    {
        GameObject effect = Instantiate(Effect, position, new Quaternion());
        effect.transform.localScale = transform.localScale;
        Object.Destroy(effect, 2f);
    }
}

