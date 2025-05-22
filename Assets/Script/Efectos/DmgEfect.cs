using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEfect : Efecto
{



    public DmgEfect(int attackValue)
    {
         intensidad = attackValue;
    }
    public override void clash(Card o)
    {
        base.clash(o);
    }
    public override void activacion(Player player)
    {
       player.damage(intensidad);
    }
}
