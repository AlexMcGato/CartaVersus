using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaWeaken : Card
{
    public int weakenvalue = 1;

    public WeakenMod weaken;
    public override void efecto()
    {
        //Debug.Log("Weaken activado");

        weaken = new WeakenMod(weakenvalue, 1);

        owner.giveModifier(weaken);
    }
}
