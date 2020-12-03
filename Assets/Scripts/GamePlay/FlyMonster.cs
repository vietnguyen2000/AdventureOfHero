using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonster : MonsterAI
{
    public bool isFlyToPlayer;
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
            if (isFlyToPlayer) FlyToPlayer();
        }
        if(AttackDelay > 0f)
        {
            Fly(new Vector2(0, 0));
        }
    }


    private void FlyToPlayer()
    {
        DirectToPlayer();
        if (CanSeePlayer())
        {
            if (distanceFormPlayer >= minDistanceAttack -0.5f)
            {
                Fly(directionToPlayer); 
            }
            else Fly(new Vector2(0, 0));
        }
        else
        {
            Fly(new Vector2(0, 0));
        }
    }
    private void Fly(Vector2 direction)
    {
        if (direction == new Vector2(0, 0))
        {
            rb.velocity = direction;
        }
        else
        {
            Vector2 dir = direction / Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
            rb.velocity = dir * m_movementSpeed;
            if (dir.x > 0) moveDirection = 1;
            else if (dir.x == 0) moveDirection = 0;
            else moveDirection = -1;
        }
    }
    private void Fly(Vector3 direction)
    {
        Vector2 directionV2 = new Vector2(direction.x, direction.y);
        Fly(directionV2);
    }
    protected override bool BeHurted(GameObject reason)
    {
        base.BeHurted(reason);
        bool isRightSide;
        if (reason.transform.position.x >= rb.position.x) isRightSide = true;
        else isRightSide = false;                                                       //Check position of reason damage 

        if (isRightSide) rb.velocity = new Vector2(-1f , 0);
        else rb.velocity = new Vector2(-1f,0);
        return true;
    }
}
