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
        if (origen.efectocarta is DmgEfect)
        {
            adversario.getCountered(origen.efectocarta.intensidad / 3 + intensidad);
            base.activacion();
        }
        else
            origen.efectocarta.activacion();


    }
    public override void activacion()
    {
        //la activacion solo vendra de cosas que no sean ataques
        // en cuyo caso no hace nada

        //adversario.damage(intensidad);
        //base.activacion();
    }
}
