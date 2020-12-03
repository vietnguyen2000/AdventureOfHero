using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Item
{
    public Vector3 Position;
    public string WhichAttack;
    public float AttackDelayTime;
    float Speed = 4f;
    GameObject Player;
    Vector2 DirectToPlayer;
    Animator anim;
    Animator PlayerAnim;
    bool isAttacked;
    Gun gun;
    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            CheckPositionPlayer(Player);
            Folow(Player);
            Attack(WhichAttack);
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected override void Active(GameObject Player)
    {
        if (gameObject.name.IndexOf("(Clone)") != -1)
        {
            gameObject.name = gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)"));
        }
        CancelInvoke("Destroy");
        PlayerAnim = Player.GetComponent<Animator>();
        gun = GetComponent<Gun>();
        rb.gravityScale = 0f;
        gameObject.layer = LayerMask.NameToLayer("Support");
        gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Support");
        this.Player = Player;
        switch ((gameObject.name))
        {
            case "Fire": Player.GetComponent<PlayerController>().FireSp = true;
                break;
            case "Wind": Player.GetComponent<PlayerController>().WindSp = true;
                    gun.setStartTransform(Player.transform);
                Debug.Log("asdsadsadnsaojdnsaoi");
                break;
            case "Ground": Player.GetComponent<PlayerController>().GroundSp = true;
                break;
            case "Ice": Player.GetComponent<PlayerController>().IceSp = true;
                break;
        }
        Debug.Log("Active success!!");

    }
    void Folow(GameObject Player)
    {
        rb.velocity = new Vector2();
        Vector3 BackPlayer = new Vector3(Player.transform.position.x + Position.x * Player.transform.localScale.x, Player.transform.position.y + Position.y, Player.transform.position.z);
        float distance = Vector3.Distance(BackPlayer, transform.position);
        if (distance >= 7f)
        {
            transform.position = BackPlayer;
            return;
        }
        if (distance >= 1f)
        {
            anim.SetBool("follow", true);
        }
        else
        {
            anim.SetBool("follow", false);
        }
        transform.position = Vector3.Slerp(transform.position, BackPlayer, Speed * Time.deltaTime);
        if (DirectToPlayer.x >= 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void CheckPositionPlayer(GameObject Player)
    {
        DirectToPlayer = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
    }
    void Attack(string WhichAttack)
    {
        if (PlayerAnim.GetBool(WhichAttack) && PlayerAnim.GetBool("isGrounded"))
        {
            if (!isAttacked)
            {
                Attack();
            }


        }
    }
    public void Attack()
    {
        isAttacked = true;
        anim.SetBool("Attack", true);
        Invoke("CancelAttack", 0.66f);
        Invoke("DelayAttack", AttackDelayTime);
    }
    void CancelAttack()
    {
        anim.SetBool("Attack", false);
    }
    void DelayAttack()
    {
        isAttacked = false;
    }
}
