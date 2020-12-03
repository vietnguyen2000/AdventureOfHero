using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float movementSpeed, slideSpeed, slideLength, jumpForce, Jump2Height,wallSlideSpeed, hurtDelayTime, BasicDamage, HP;
    public int Score;
    public bool FireSp, IceSp, GroundSp, WindSp;
    public PlayerData()
    {
        SetDefault();
    }
    public void SetDefault()
    {
        movementSpeed = 6.5f;
        slideSpeed = 10f;
        slideLength = 0.5f;
        jumpForce = 23f;
        Jump2Height = 0.85f;
        wallSlideSpeed = 5f;
        hurtDelayTime = 0.8f;
        Score = 0;
        HP = 5f;
        BasicDamage = 1f;
    }
}