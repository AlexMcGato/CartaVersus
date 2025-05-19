using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    private bool toPlay = false;
    public Player player;

    public GameManager gm;
  
    public void selectCard()
    {
        //Debug.Log(toPlay);

        toPlay = !toPlay;

        if (toPlay)
        {
            //Debug.Log("Isplayed");
            transform.position += Vector3.up *50;
            player.jugada.Add(this);
        }
        else
        {
            transform.position -= Vector3.up *50;
            player.jugada.Remove(this);
        }
    }

    public virtual void efecto()
    {

    }
    
}
