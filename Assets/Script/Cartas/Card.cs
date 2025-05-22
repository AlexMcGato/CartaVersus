using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public int coste = 2;
    private bool toPlay = false;
    public Player owner;

    public GameManager gm;

    public Efecto efectocarta;
   
    public void selectCard()
    {
        if(owner.stamina < coste)
           return;

        Debug.Log(toPlay);

        toPlay = !toPlay;

        if (toPlay)
        {
            owner.stamina -= coste;
            //Debug.Log("Isplayed");
            transform.position += Vector3.up *50;
            owner.jugada.Add(this);
        }
        else
        {
            owner.stamina += coste;
            transform.position -= Vector3.up *50;
            owner.jugada.Remove(this);
        }


    }

    //efecto cuando se recibe
    public virtual void clash(Card origen)
    {
        efectocarta.clash(origen); 
    }
    
    
    public virtual void activacion(Player player)
    {
      efectocarta.activacion(player);
    }

}
