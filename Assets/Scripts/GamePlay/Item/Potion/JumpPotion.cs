using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotion : Potion
{
    protected override void Active(GameObject Player)
    {
        if (Player.GetComponent<PlayerController>().jumpForce <= 30f)
        {
            Player.GetComponent<PlayerController>().jumpForce += Value;
            MakeEffect(Player, "Jump");
        }
        else Player.GetComponent<PowerValue>().Score += (int)level * 10;
    }
}
