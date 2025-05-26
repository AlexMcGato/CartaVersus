using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversario : Player
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
        gameManager.jugadaAdversario = jugada;
    }

    //metodos obsoletos por el sistema de efectos
    /*
    public override void attack(int value)
    {
        gameManager.foeAttack = value;
    }
    public override void protect(int value)
    {
        gameManager.foeProt = value;
    }
    public override void counter(int value)
    {
        gameManager.foeCounter = value;
    }
    */
}
