using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEfect : Efecto
{



    public DmgEfect(int intensidad, Player jugador, Player adversario) : base(intensidad, jugador, adversario)
    {
        
    }
    public override void clash(Card origen)
    {
        //comprueba el main efect de la carta que le choca
        //si es counter recibe counter
        if (origen.efectocarta is CounterEfect)
        {
            player.damage(intensidad/3 + origen.efectocarta.intensidad);
            base.clash(origen);
            return;
        }

        //si es proteccion activa primero la proteccion
        if (origen.efectocarta is ProtEfect)
        {
            origen.efectocarta.activacion();
            activacion();
        }
        
       
        
    }
    public override void activacion()
    {
        adversario.damage(intensidad);
        base.activacion();
    }
}
