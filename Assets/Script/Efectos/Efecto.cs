using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efecto
{
    //carta a que esta asociada
    public Card asociada;

    //player al que se le aplica
    public Player player;
    //intensidad == numero de activaciones/turnos en caso de ser necesario
    public int intensidad = 0;
  
    public virtual void clash(Card origen)
    {

    }
    public virtual void activacion(Player player)
    {

    }




    
}
