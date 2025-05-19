using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CartaAtaque : Card
{
    public int value = 5;

    public int coste = 2;

     public override void efecto()
     {
        gm.playerAttack += 5;
     }
}
