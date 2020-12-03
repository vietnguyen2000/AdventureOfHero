using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MoveLeftCycle
{
    // Start is called before the first frame update
    public float timeAlive, startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    protected override void Update()
    {
        timeAlive = Time.time - startTime;
        transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}