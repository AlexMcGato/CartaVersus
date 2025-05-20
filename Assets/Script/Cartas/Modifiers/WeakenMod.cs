using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeakenMod : ModificadorCarta
{
    public int weakenValue = 1;
    

    
    public WeakenMod(int weakenValue)
    {
      this.weakenValue = weakenValue;
    }
    public WeakenMod(int weakenValue, int turncounter, int cardcounter = 99)
    {
        this.weakenValue = weakenValue;
        this.turncounter = turncounter;
        this.cardcounter = cardcounter;
    }

    public override CartaAtaque applyMod(CartaAtaque carta)
    {
       carta.attackvalue -= weakenValue;
        
       this.cardcounter--;
       if (this.cardcounter <= 0 )
            player.cardMods[this.category].Remove(this);

       return carta;
    }
}
