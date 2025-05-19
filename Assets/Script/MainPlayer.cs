using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player
{
    public override void damage(int damage)
    {
        if (stamina <= 0)
        {
            hp--;
            if (hp <= 0)
                gameManager.ganador = gameManager.combatiente[0];
        }
    }

    public override void jugarMano()
    {

        gameManager.jugadaPlayer = jugada;
    }
}
