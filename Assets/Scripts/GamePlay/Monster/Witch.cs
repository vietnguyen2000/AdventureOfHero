using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : GroundMonster
{
    public bool Spawn;
    public GameObject WhichEnemySpawn;

    private bool Spawned;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        SpawnGoul();
        Spawned = Spawned && Spawn;
    }
    private void SpawnGoul()
    {
        if (CanSeePlayer() && distanceFormPlayer > minDistanceAttack)
        {
            if (!ActiveMoveAway)
            {
                DirectToPlayer();
                anim.SetBool("Spawn", true);
                if (Spawn)
                {
                    if (!Spawned)
                    {
                        WhichEnemySpawn.transform.localScale = transform.localScale;
                        Instantiate(WhichEnemySpawn, new Vector3(transform.position.x + 2f * transform.localScale.x, transform.position.y + transform.position.z), new Quaternion());
                        Spawned = true;
                    }
                }
            }
            else
            { 
                anim.SetBool("Spawn", false);
            }
        }
        else
        {
            anim.SetBool("Spawn",false);
        }
    }
}
