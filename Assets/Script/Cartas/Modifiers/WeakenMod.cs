using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeakenMod : ModificadorCarta
{
    
    

    
    public WeakenMod()
    {
      
    }
    
    //obsoleto
    /*
    public override CartaAtaque applyMod(CartaAtaque carta)
    {
       carta.attackvalue -= weakenValue;
        
       this.cardcounter--;
       if (this.cardcounter <= 0 )
            player.cardMods[this.category].Remove(this);

       return carta;
    }
    */
}
