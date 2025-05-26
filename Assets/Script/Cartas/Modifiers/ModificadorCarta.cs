using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadorCarta 
{
    public Player player;

    //0= attack
    //1= prot
    //2= counter

    public int category = 0;


    public int cardcounter = 99;
    public int turncounter = 99;

    public int baseCardcounter = 99;
    public int baseTurncounter = 99;

    public int value = 0;

    public virtual void applyMod()
    {
       this.cardcounter --; 
    }

    public ModificadorCarta(int cardCounter, int turncounter)
    {
        this.baseCardcounter = cardCounter;
        this.baseTurncounter = turncounter;
    }
    public ModificadorCarta()
    {
       
    }
    /*
    public virtual CartaAtaque applyMod(CartaAtaque carta)
    {
        return carta;
    }
    public virtual CartaCounter applyMod(CartaCounter carta)
    {
        return carta;
    }
    public virtual CartaProt applyMod(CartaProt carta)
    {
        return carta;
    }
     */

}
