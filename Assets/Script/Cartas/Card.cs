using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int baseCost = 2;
    public int coste = 2;

    //control de si esta seleccionada para jugarla
    private bool toPlay = false;

    public Player owner;
    public Player rival;

    public GameManager gm;

    public Efecto efectocarta;
   
    public Card(GameManager gm, Player owner, Efecto efecto)
    {
        this.gm = gm;
        this.owner = owner;
        this.efectocarta = efecto;
    }
    public Card()
    {
        
    }
    public void selectCard()
    {
        if(owner.mana < coste)
           return;

        Debug.Log(toPlay);

        toPlay = !toPlay;

        if (toPlay)
        {
            owner.manaSpendPreview += coste;
            //Debug.Log("Isplayed");
            transform.position += Vector3.up *50;
            owner.jugada.Add(this);
        }
        else
        {
            owner.manaSpendPreview -= coste;
            transform.position -= Vector3.up *50;
            owner.jugada.Remove(this);
        }


    }

    //efecto cuando se recibe
    public virtual void clash(Card origen)
    {
        efectocarta.clash(origen); 
    }
    
    
    public virtual void activacion()
    {
      efectocarta.activacion();
    }

    public void emptyClash(Card origen)
    {

    }

    public void emptyActivar()
    {

    }
}
