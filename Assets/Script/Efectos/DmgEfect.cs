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
        //si es counter recibe daño
        if (origen.efectocarta is CounterEfect)
        {
            player.getCountered(intensidad/3 + origen.efectocarta.intensidad);
            base.clash(origen);
            return;
        }

        //si te viene el clash aqui, quiere decir que has perdido la iniciativa y mientras que no sea un counter, el resto de cosas activan antes que el ataque
   
         origen.activacion();
         activacion();
       
        
       
        
    }
    public override void activacion()
    {
        adversario.damage(intensidad);
        base.activacion();
    }
}
