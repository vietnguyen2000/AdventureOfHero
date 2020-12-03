using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMonster : MonsterAI
{

    public bool isMoveAround;
    public bool isMoveToPlayer;
    public bool isBoostSpeedWhenMoveToPlayer;
    public bool isMoveAwayPlayer;

    protected bool ActiveMoveAway;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (AttackDelay <= 0f && m_KnockBackDelay <= 0f)
        {
            if (isMoveAround && isMoveToPlayer)
            {
                if (!CanSeePlayer())
                {
                    MoveAround();
                }
                else
                {
                    MoveToPlayer();
                }
            }
            else if (isMoveAround || isMoveToPlayer)
            {
                if (isMoveAround) MoveAround();
                if (isMoveToPlayer && !ActiveMoveAway) MoveToPlayer();


            }
            if (isMoveAwayPlayer) MoveAwayPlayer();
        }
        else movementInputDirection = 0;
    }
    protected override bool CanSeePlayer()
    {
        return (base.CanSeePlayer() && Mathf.Abs(directionToPlayer.y) <= 6f);
    }
    private void CheckCanMove()
    {
        bool m_isTouchingWall = Physics2D.OverlapBox(new Vector2((CheckSurround.transform.position.x + (Collider.offset.x + wallCheckDistance) * transform.localScale.x * Mathf.Abs(moveDirection)), CheckSurround.transform.position.y + Collider.offset.y), new Vector2(wallCheckDistance, Collider.size.y - 0.1f), 0, groundLayer);
        //bool m_isTouchingTrap = Physics2D.OverlapBox(new Vector2(CheckSurround.transform.position.x + (Collider.offset.x + wallCheckDistance) * moveDirection, CheckSurround.transform.position.y + Collider.offset.y), new Vector2(wallCheckDistance, Collider.size.y - 0.1f), 0, 1 << LayerMask.NameToLayer("Trap"));
        bool isNextGround = Physics2D.OverlapBox(new Vector2(CheckSurround.transform.position.x + (Collider.size.x) * transform.localScale.x, CheckSurround.transform.position.y), new Vector2(0.25f, 0.5f), 0, groundLayer);
        canMove = isNextGround && !m_isTouchingWall;
    }
    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();
    //    Gizmos.DrawWireCube(new Vector3((CheckSurround.transform.position.x + (Collider.offset.x + wallCheckDistance) * transform.localScale.x * Mathf.Abs(moveDirection)), CheckSurround.transform.position.y + Collider.offset.y, 0), new Vector3(wallCheckDistance, Collider.size.y - 0.1f, 0));
    //    Gizmos.DrawWireCube(new Vector3(CheckSurround.transform.position.x + (Collider.size.x) * transform.localScale.x, CheckSurround.transform.position.y, 0), new Vector3(0.25f, 0.5f, 0));
    //}

    protected void MoveAround()
    {
        CheckCanMove();
        if (!canMove)
        {
            movementInputDirection = -movementInputDirection;
            moveDirection *= -1;
        }
        movementInputDirection = moveDirection;
        if (isBoostSpeedWhenMoveToPlayer) m_movementSpeed = movementSpeed;
    }
    protected void MoveToPlayer()
    {
        DirectToPlayer();
        CheckCanMove();
        if (isBoostSpeedWhenMoveToPlayer)
        {
            if (CanSeePlayer()) m_movementSpeed = movementSpeed * 2f;
            else m_movementSpeed = movementSpeed;
        }
        if (canMove && CanSeePlayer())
        {
            if(Mathf.Abs(directionToPlayer.x) >= minDistanceAttack -0.5f)
            {

                movementInputDirection = (transform.localScale.x)/Mathf.Abs(transform.localScale.x);
            }
            else
            {
                movementInputDirection = 0;
            }
        }
        else
        {
            movementInputDirection = 0;
        }
        moveDirection = (int)((transform.localScale.x) / Mathf.Abs(transform.localScale.x));
    }
    private void MoveAwayPlayer()
    {
        
        if(distanceFormPlayer <= 3f)
        {
            ActiveMoveAway = true;
        }
        if (ActiveMoveAway)
        {
            DirectToPlayer();
            Flip();
            moveDirection = (int)((transform.localScale.x) / Mathf.Abs(transform.localScale.x));
            CheckCanMove();
            movementInputDirection = (transform.localScale.x) / Mathf.Abs(transform.localScale.x);
            if (distanceFormPlayer >= 6f || !canMove)
            {
                Flip();
                ActiveMoveAway = false; 
                movementInputDirection = 0;
                moveDirection = 0;
            }
        }
    }
}
