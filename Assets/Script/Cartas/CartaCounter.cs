using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaCounter : Card
{
    public int counterValue = 5;

    

    public override void clash(Card origen)
    {
        owner.counter(counterValue);
    }
}
