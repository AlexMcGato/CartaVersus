using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaProt : Card
{
    public int protectValue = 5;

    

    public override void efecto()
    {
        owner.protect(protectValue);
    }
}
