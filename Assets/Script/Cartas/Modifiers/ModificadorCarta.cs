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

    public int value = 0;

    

    //target indica a quien se le pone el modificador y apply indica quien recibe el efecto
    //ejemplo modificador de debilidad que tiene como objetivo el enemigo seria false targetSelf y true applySelf 
    //pero un efecto de dañar al rival cada turno seria targetSelf true y applySelf false
    public bool targetSelf;
    public bool applyself;

    public int cardcounter = 99;
    public int turncounter = 99;

    public int baseCardcounter = 99;
    public int baseTurncounter = 99;

    //puede haber modificadores para los valores de la carta o al coste u otros aspectos
    public Dictionary<int, List<int>> valores = new Dictionary<int, List<int>>();

    public virtual void applyMod()
    {


        this.cardcounter--;
    }

    public virtual Card applyMod(Card carta)
    {
        

        this.cardcounter --;
        return carta;
    }

    public ModificadorCarta(int category, int cardCounter, int turncounter, bool targetSelf, bool applySelf)
    {
        this.category = category;
        this.baseCardcounter = cardCounter;
        this.baseTurncounter = turncounter;
        this.targetSelf = targetSelf;
        this.applyself = applySelf;
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
