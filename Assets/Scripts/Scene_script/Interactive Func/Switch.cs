using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactive
{
    public bool on = false;
    private SpriteRenderer sprite;

    public string source;

    public int index;
    protected override void Start()
    {
        base.Start();
        canDo = true;
    }

    protected override void DoWork(object arg = null)
    {
        if (!on)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = Resources.LoadAll<Sprite>("GameObject/Scene/" + source)[index];

            on = true;
            keyUpSprite.SetActive(false);
            canDo = false;
        }
    }
}
