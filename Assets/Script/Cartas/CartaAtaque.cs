using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CartaAtaque : Card
{
    public int attackvalue = 5;

   

   

     public override void clash(Card origen)
     {
        
        owner.attack(attackvalue);
     }

     public override void activacion(Player player)
     {

        efectocarta = new DmgEfect(attackvalue);
        efectocarta.activacion(player);
     }

}
