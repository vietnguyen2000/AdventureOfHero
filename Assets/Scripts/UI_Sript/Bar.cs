using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public enum TypeValue { HP, Shield };
    // Start is called before the first frame update
    GameObject Player;
    SpriteRenderer sprite;
    public Sprite[] BarSprite;

    public TypeValue typeValue;
    float lastValue;
    float curValue;
    bool isEffected;
    void Start()
    {
        Player = GameObject.Find("Player");
        if (typeValue == TypeValue.HP)
            curValue = Player.GetComponent<PowerValue>().HP;
        else if (typeValue == TypeValue.Shield)
            curValue = Player.GetComponent<PowerValue>().Shield;
        lastValue = curValue;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player = GameObject.Find("Player");
        if (typeValue == TypeValue.HP)
            curValue = Player.GetComponent<PowerValue>().HP;
        else if (typeValue == TypeValue.Shield)
            curValue = Player.GetComponent<PowerValue>().Shield;
        if (curValue != lastValue)
        {
            if (!isEffected)
            {
                isEffected = true;
                MakeEffectChange();
                Invoke("setLastValue", 0.6f);
            }
        }


    }
    void MakeEffectChange()
    {
        setSpriteCurValue();
        Invoke("setSpriteLastValue", 0.1f);
        Invoke("setSpriteCurValue", 0.2f);
        Invoke("setSpriteLastValue", 0.4f);
        Invoke("setSpriteCurValue", 0.6f);
    }
    void setSpriteLastValue()
    {
        sprite.sprite = BarSprite[(int)lastValue];
    }
    void setSpriteCurValue()
    {
        sprite.sprite = BarSprite[(int)curValue];
    }
    void setLastValue()
    {
        lastValue = curValue;
        isEffected = false;
    }
}
