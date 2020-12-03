using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : Controller
{
    public bool isDirectToPlayer;
    public bool isAttackIfNear;
    public float minDistanceAttack = 2f;
    public float AttackDelayTime = 1f;
    public float DistanceCanSeePlayer = 9999;


    protected GameObject Target;
    protected Vector3 directionToPlayer;
    protected float distanceFormPlayer;
    protected int moveDirection=1;


    private GameObject Slash01;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Target = GameObject.FindGameObjectWithTag("Player");
        Slash01 = Resources.Load<GameObject>("Effect/Slash/Slash01");
        isFacingRight = true;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckTargetPosition();
        CheckDistanceFormPlayer();
        if (m_KnockBackDelay <= 0f)
        {
            if (isAttackIfNear) AttackIfNearTarget();
            if (isDirectToPlayer) DirectToPlayer();
        }
        else
        {
            resetValue();
        }
    }
    private void CheckTargetPosition()
    {
        if (Target != null) directionToPlayer = Target.transform.position - transform.position; //check direction to player
    }
    protected virtual bool CanSeePlayer()
    {
        return (distanceFormPlayer <= DistanceCanSeePlayer);
    }

    protected void DirectToPlayer() //this function will auto face to player
    {
        if (distanceFormPlayer <= DistanceCanSeePlayer && AttackDelay <=0f)
        {
            if (isFacingRight && directionToPlayer.x < 0 || !isFacingRight && directionToPlayer.x > 0)
            {
                Flip();
            }
        }
    }
    private void CheckDistanceFormPlayer() //this function will check distance this monster form player
    {
        distanceFormPlayer = Vector3.Distance(transform.position, Target.transform.position);
    }
    protected void AttackIfNearTarget() //this function will attack if the distance to player < distance attack
    {
        if ( distanceFormPlayer< Mathf.Abs(minDistanceAttack * transform.localScale.x) && CanSeePlayer() )
        {


            if (isFacingRight && directionToPlayer.x > 0 || !isFacingRight && directionToPlayer.x < 0)
            {
                if (AttackDelay <= 0f)
                {
                    AttackDelay += AttackDelayTime;
                    anim.SetFloat("AttackDelay", AttackDelay);
                }
            }
        }
        if (AttackDelay >= 0)
        {
            AttackDelay -= Time.deltaTime;
            anim.SetFloat("AttackDelay", AttackDelay);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 15 && collision.isTrigger && powerValue.HP>0)
        {
            var temp = Slash01.GetComponent<ParticleSystem>().main;
            temp.startRotation = Random.Range(0f, 180f);
            GameObject Slash = Instantiate(Slash01, new Vector3 (transform.position.x,transform.position.y + Collider.offset.y,0), new Quaternion(0, 0, 0, 0)); 
            //make effect slash
            BeHurted(collision.gameObject);
            Object.Destroy(Slash,1f);
            SFXManager.PlaySoundEffectOneShot(SFXManager.Slash);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 19 && collision.isTrigger && powerValue.HP > 0)
        {
            BeHurted(collision.gameObject);
        }
    }
    protected void resetValue()
    {
        AttackDelay = 0f;
        movementInputDirection = 0;
    }
}
