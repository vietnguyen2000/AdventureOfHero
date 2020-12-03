using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThroughTheGate : Interactive
{
    public Vector3 end_point;
    public GameObject[] guard;
    private void Update()
    {
        CheckCanDo();
    }
    void CheckCanDo()
    {
        bool is_over = true;

        if (guard.Length != 0)
        {
            foreach (GameObject i in guard)
            {
                is_over = is_over && (i == null);
            }
        }
        if (is_over) canDo = true;
    }
    protected override void DoWork(object arg = null)
    {

        Collider2D player = (Collider2D)arg;
        if (canDo)
        {
            player.transform.position = end_point;
            MainCam.transform.position = end_point;
        }
    }
}
