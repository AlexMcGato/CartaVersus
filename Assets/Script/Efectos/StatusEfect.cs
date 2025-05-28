using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEfect : Efecto
{
    public ModificadorCarta ModificadorCarta;


    public StatusEfect(int intensidad, Player jugador, Player adversario) : base(intensidad, jugador, adversario)
    {

    }
    public StatusEfect(int intensidad, Player jugador, Player adversario, ModificadorCarta modifier) : base(intensidad, jugador, adversario)
    {
        this.ModificadorCarta = modifier;
    }
    public override void clash(Card origen)
    {
        player.protect(intensidad);
        base.activacion();
        origen.activacion();

    }
    public override void activacion()
    {
        player.protect(intensidad);
        base.activacion();
    }
}
