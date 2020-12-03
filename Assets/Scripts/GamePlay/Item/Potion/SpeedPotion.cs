using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion :Potion
{
    protected override void Active(GameObject Player)
    {
        if (Player.GetComponent<PlayerController>().movementSpeed <= 9f)
        {
            Player.GetComponent<PlayerController>().movementSpeed += Value;
            Player.GetComponent<PlayerController>().slideSpeed += Value;
            MakeEffect(Player, "Speed");
        }
        else { Player.GetComponent<PowerValue>().Score += (int)level * 10; }


    }
}
