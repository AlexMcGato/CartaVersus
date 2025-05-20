using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CartaAtaque : Card
{
    public int attackvalue = 5;

   

   

     public override void efecto()
     {
        
        owner.attack(attackvalue);
     }
}
