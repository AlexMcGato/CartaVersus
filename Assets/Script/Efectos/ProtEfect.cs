using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtEfect : Efecto
{
    
    public ProtEfect(int value) 
    {
       intensidad = value;
    }
    public override void clash(Card origen)
    {
        player.protect(intensidad);
        origen.efectocarta.activacion(player);
    }
}
