using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEfect : Efecto
{
    public CounterEfect(int intensidad, Player jugador, Player adversario) : base(intensidad, jugador, adversario)
    {
    }

    public override void clash(Card origen)
    {
        if(origen.efectocarta is DmgEfect)
        {
            adversario.damage(intensidad + origen.efectocarta.intensidad);
            base.activacion();
        }


    }
    public override void activacion()
    {
        adversario.damage(intensidad);
        base.activacion();
    }
}
