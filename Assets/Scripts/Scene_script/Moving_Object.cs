using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Object : MonoBehaviour
{
    public Vector3 start_point;
    public Vector3 end_point;
    public bool repeat = true;
    public int num_of_step;

    protected Vector3 cur_point;
    protected int step;
    protected bool direction;
    
    void Start()
    {
        this.cur_point = new Vector3();
        this.cur_point = this.start_point;

        this.direction = true;
        this.step = this.num_of_step;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(gameObject.transform, true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(gameObject.transform, true);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 oldPosition = collision.transform.position;
            collision.collider.transform.SetParent(null);
            collision.transform.position = oldPosition;
        }
    }
    //asdsadsad
    // Update is called once per frame
    protected void Update()
    {
        if (Time.timeScale != 0)
        {
            if (this.step > 0)
            {
                if (this.direction)
                {
                    this.cur_point.x -= ((start_point.x - end_point.x) / num_of_step);
                    this.cur_point.y -= ((start_point.y - end_point.y) / num_of_step);
                    this.cur_point.z -= ((start_point.z - end_point.z) / num_of_step);
                }
                else
                {
                    this.cur_point.x += ((start_point.x - end_point.x) / num_of_step);
                    this.cur_point.y += ((start_point.y - end_point.y) / num_of_step);
                    this.cur_point.z += ((start_point.z - end_point.z) / num_of_step);
                }

                this.step--;
                this.transform.position = this.cur_point;
            }
            else if (this.repeat)
            {
                this.step = this.num_of_step;
                this.direction = !this.direction;
            }
        }
    }
}
