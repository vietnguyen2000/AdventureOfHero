using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Object : MonoBehaviour
{
    private Rigidbody2D body;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            body.gravityScale = 6;
        }
    }
}

/*
 *public float acceleration;
    public Vector3 directon;
    private bool is_stop = false;
    private float start_time;

    private LayerMask Ground;
    void Start()
    {
        if (this.directon.x > 0) this.directon.x = 1;
        else if (this.directon.x < 0) this.directon.x = -1;
        else this.directon.x = 0;

        if (this.directon.y > 0) this.directon.y = 1;
        else if (this.directon.y < 0) this.directon.y = -1;
        else this.directon.y = 0;

        if (this.directon.z > 0) this.directon.z = 1;
        else if (this.directon.z < 0) this.directon.z = -1;
        else this.directon.z = 0;

        Ground = 1 << LayerMask.NameToLayer("Ground");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !is_stop)
        {
            this.start_time = Time.time;
            is_stop = true;
        }
    }

    void Update()
    {
        if (this.is_stop)
        {
            if (!Physics2D.OverlapBox(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(0.05f, 0.25f), 0, Ground))
            {
                this.transform.Translate(directon * acceleration * (-this.start_time + Time.time)/10f);
            }
        }
    } 
 */
