using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int Score;
    protected override void Start()
    {
        base.Start();
        Collect = SFXManager.CoinCollect;
    }
    protected override void Active(GameObject Player)
    {
        //Adding score code in here
        Player.GetComponent<PowerValue>().Score += this.Score;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer== 9)
        {
            SFXManager.PlaySoundEffectOneShot(SFXManager.CoinBounce);
        }
    }
}
