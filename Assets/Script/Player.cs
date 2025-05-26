using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public int defensa = 1;
    public int stamina = 10;
    public int hp = 3;
    public int prot = 0;
    
    public List<Card> deck = new List<Card>();
    public Transform[] espacios;
    public bool[] espacioslibres;

    public List<Card> jugada = new List<Card>();


    //0= attack
    //1= prot
    //2= counter

    public Dictionary<int, List<ModificadorCarta>> cardMods = new Dictionary<int, List<ModificadorCarta>>()
    {
        {0, new List<ModificadorCarta>() },
        {1, new List<ModificadorCarta>() },
        {2, new List<ModificadorCarta>() },
    };


    public GameManager gameManager;

    private void Start()
    {


        foreach(Card card in deck)
        {
            card.gm = gameManager;
            card.owner = this;
        }

        espacioslibres = new bool[espacios.Length];

        for (int i = 0; i < espacioslibres.Length; i++)
            espacioslibres[i] = true;
    }

    public void SacaCarta()
    {

        bool cointoss = new System.Random().Next(0, 2) == 0 ? true : false;

        Debug.Log("Cointoss: " + cointoss); 

        // Debug.Log("sacacarta");
        // Console.WriteLine(deck.Count);
        if (deck.Count >= 1)
        {

           // Debug.Log("sacacarta");
           // Console.WriteLine(deck.Count);

            Card carta = deck[UnityEngine.Random.Range(0, deck.Count)];
           

            for (int i = 0; i < espacioslibres.Length; i++)
            {
                if (espacioslibres[i] == true)
                {
                    carta.gameObject.SetActive(true);
                    carta.transform.position = espacios[i].position;
                    espacioslibres[i] = false;
                    deck.Remove(carta);
                    return;

                }

            }

        }
    }

    public virtual void attack(int value)
    {

    }
    public virtual void counter(int value)
    {

    }
    public virtual void protect(int value)
    {
          prot = value;
    }


    public virtual void jugarMano()
    {
        List<Card> resultCards = new List<Card>();

        foreach (Card card in jugada)
        {

            //reset de carrtas por si se ha acabado el efecto
            card.efectocarta.intensidad = card.efectocarta.intensidadBase;

            foreach (Efecto secundario in card.efectocarta.adicionales)
            {
                secundario.intensidad = secundario.intensidadBase;
            }

            //aplicacion de los efectos
            foreach (var entry in cardMods)
            {
                
                //efectio principal
                if (card.efectocarta.category == entry.Key)
                {
                    foreach(ModificadorCarta modifier in entry.Value)
                    {
                       card.efectocarta.modifyEfect(modifier.value);
                       modifier.applyMod();
                    }
                }

                //efectos secundarios
                foreach(Efecto secundario in card.efectocarta.adicionales)
                {
                    if (card.efectocarta.category == entry.Key)
                    {
                        foreach (ModificadorCarta modifier in entry.Value)
                        {
                            card.efectocarta.modifyEfect(modifier.value);
                            modifier.applyMod();
                        }
                    }
                }

            }

        }
        
    }

    public void nextCardResolution()
    {
        this.prot = 0;
    }
    

    public virtual void damage(int damage)
    {
        stamina -= damage - defensa- prot;

        if (prot > 0)
            prot--;


        if (stamina <= 0)
        {
            hp--;
            
        }
    }

    public virtual void giveModifier(ModificadorCarta weaken)
    {
       
    }
}
