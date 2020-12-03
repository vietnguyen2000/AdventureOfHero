using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReturnSwitch : Interactive
{
    public int start, end;
    public string path;
    public int delay_frame;
    public Switch[] keys;

    private SpriteRenderer sprite;

    public bool on = false;
    public int counter = -1;
    protected override void Start()
    {
        base.Start();
        SpriteRenderer temp = GetComponent<SpriteRenderer>();
        temp.color = new Color(1, 0.4858f, 0.4858491f);
        canDo = true;
    }
    protected override void DoWork(object arg = null)
    {
        bool canSwitch = true;

        foreach (Switch i in keys)
        {
            if (!i.on) canSwitch = false;
        }
        

        if (canSwitch)
        {
            counter = delay_frame;
            sprite.sprite = Resources.LoadAll<Sprite>("GameObject/Scene/" + path)[end];
            on = true;
            keyUpSprite.SetActive(false);
            canDo = false;
        }
    }

    private void Update()
    {
        if (counter <= 0)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.LoadAll<Sprite>("GameObject/Scene/" + path)[start];
            on = false;
        }
        else
        {
            counter--;
        }
    }
}
