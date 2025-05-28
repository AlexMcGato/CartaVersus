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

    public void Start()
    {
        efectocarta.player = owner;
        efectocarta.adversario = rival;
    }
    public void selectCard()
    {
        if(owner.mana < coste)
           return;

        //Debug.Log(toPlay);

        
        if(owner.mana >= coste + owner.manaSpendPreview)
        {
            toPlay = !toPlay;

            if (toPlay)
            {
                
                owner.manaSpendPreview += coste;
                //Debug.Log("Isplayed");
                transform.position += Vector3.up * 50;
                owner.espacioslibres[owner.mano.IndexOf(this)]= true;
                owner.jugada.Add(this);
                owner.updateMana();

            }
            else
            {
                owner.manaSpendPreview -= coste;
                transform.position -= Vector3.up * 50;
                owner.espacioslibres[owner.mano.IndexOf(this)] = false;
                owner.jugada.Remove(this);
                owner.updateMana();
            }
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
        efectocarta.emptyClash(origen);
    }

    public void emptyActivar()
    {

    }
}
