using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop_up_Object : Moving_Object
{  
    void Start()
    {
        this.cur_point = new Vector3();
        this.cur_point = this.start_point;
        this.repeat = false;
        this.step = -1;

        this.direction = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.step == -1 && collision.tag == "Player") this.step = this.num_of_step;
    }
}
