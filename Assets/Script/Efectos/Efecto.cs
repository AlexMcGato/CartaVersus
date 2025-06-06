using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Efecto :MonoBehaviour
{
    

    //player propietario
    public Player player;

    //necesito conocer ambos por si hay cartas con efectos tanto positivos como negativos
    public Player adversario;

   
    public int intensidad = 0;
    public int intensidadBase = 0;

    //0= attack
    //1= prot
    //2= counter
    //3= status
    public int category = 0;
    
    public List<Efecto> adicionales = new List<Efecto>();

    public Efecto(int intensidad, Player jugador, Player adversario)
    {
        this.intensidad = intensidad;
        this.intensidadBase = intensidad;
        this.adversario = adversario;
        this.player = jugador;
    }
    public Efecto() { }

    public void setOwner(Player owner)
    {
       this.player = owner;  
    }
    public void setAdversario(Player rival)
    {
         this.adversario = rival;
    }


    public virtual void clash(Card origen)
    {

        foreach (Efecto secundario in this.adicionales) 
        { 
           secundario.clash(origen);
        }
    }
    public virtual void activacion()
    {
        foreach (Efecto secundario in this.adicionales)
        {
            secundario.activacion();
        }
    }
     
    

    public void emptyClash(Card origen)
    {
        if (origen.efectocarta is not CounterEfect)
            origen.efectocarta.activacion();
    }
    public void emptyActivate() { }

    public virtual void modifyEfect(int modifier)
    {
        this.intensidad = intensidadBase + modifier;
    }


}
