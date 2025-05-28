using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtEfect : Efecto
{
    
    public ProtEfect(int intensidad, Player jugador, Player adversario) : base(intensidad, jugador, adversario)
    {
       
    }
    public override void clash(Card origen)
    {
        //proteccion siempre activa antes que el resto
        this.activacion();
        
        origen.activacion();
        
       
        
    }
    public override void activacion()
    {
        player.protect(intensidad);
        base.activacion();
    }

   
}
