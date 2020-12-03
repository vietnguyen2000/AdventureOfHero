using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : Interactive
{
    public GameObject[] Keys;
    public bool scroll_down = false;
    public GameObject gate_switch;

    public Vector3 start_point;
    public Vector3 end_point;
    public int num_of_step;
    private int cur_step = 0;
    private bool allowed = false;

    private Vector3 cur_point;
    protected override void Start()
    {
        cur_point = start_point;
    }
    protected override void DoWork(object arg = null)
    {
        
    }

    protected virtual void Update()
    {
        if (scroll_down && cur_step < num_of_step && allowed)
        {
            cur_point += (end_point - start_point) / num_of_step;
            cur_step++;

            this.transform.position = cur_point;
        }
        else
        {
            if (Keys != null)
            {
                bool is_destroy = true;

                foreach (GameObject i in Keys)
                {
                    if (i) is_destroy = is_destroy && (i.GetComponent<Switch>().on);
                }

                if (is_destroy)
                {
                    scroll_down = true;
                }
            }
            else scroll_down = true;

            if (gate_switch && gate_switch.GetComponent<AutoReturnSwitch>())
                allowed = gate_switch.GetComponent<AutoReturnSwitch>().on;
        }
    }
}
