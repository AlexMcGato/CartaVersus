using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player
{
    public override void damage(int damage)
    {
        base.damage(damage);
        
        if (hp <= 0)
            gameManager.ganador = gameManager.combatiente[0];
        
    }

    public override void jugarMano()
    {
        base.jugarMano();
        gameManager.jugadaPlayer = jugada;
        gameManager.resolver();
    }


    //metodos obsoletos por el sistema de efectos
    /*
    public override void attack(int value)
    {
        gameManager.playerAttack = value;
    }
    public override void protect(int value)
    {
       base.protect(value);
    }
    public override void counter(int value)
    {
        gameManager.playerCounter = value;
    }

    public override void giveModifier(ModificadorCarta modifier)
    {
       gameManager.modifierToFoe(modifier);
    }
    */
}
