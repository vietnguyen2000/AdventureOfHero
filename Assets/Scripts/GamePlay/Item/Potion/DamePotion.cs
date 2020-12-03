using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamePotion : Potion
{
    protected override void Active(GameObject Player)
    {
        if (Player.GetComponent<PowerValue>().basicDamage <= 5f)
        {
            Player.GetComponent<PowerValue>().basicDamage += Value;
            MakeEffect(Player, "Dame");
        }
        else
        {
            Player.GetComponent<PowerValue>().Score += (int)level * 10;
        }

    }
}
