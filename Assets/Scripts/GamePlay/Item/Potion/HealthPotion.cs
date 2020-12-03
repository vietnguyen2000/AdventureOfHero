using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    protected override void Active(GameObject Player)
    {
        if (Player.GetComponent<PowerValue>().HP < Player.GetComponent<PowerValue>().startHP)
        {
            Player.GetComponent<PowerValue>().Health(Value);
            MakeEffect(Player, "Health");

        }
        else { Player.GetComponent<PowerValue>().Score += (int)level * 10; }

    }
}
