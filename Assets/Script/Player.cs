using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player rival;

    public int defensa = 1;
    public int stamina = 10;
    public int hp = 3;
    public int prot = 0;
    
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();
    public Transform[] espacios;
    public Transform[] espaciosJugado;
    public Transform discardSpace;

    public bool[] espacioslibres;

    public List<Card> jugada = new List<Card>();


    //00+ = attack
    //10+ = prot
    //20+ = counter

    public Dictionary<int, List<ModificadorCarta>> cardMods = new Dictionary<int, List<ModificadorCarta>>()
    {
        //energia ataques
        {00, new List<ModificadorCarta>() }
        ,
        //modificar valor ataque
         {01, new List<ModificadorCarta>() }
        ,
         //energia prot
        {10, new List<ModificadorCarta>() }
        ,
        //modificar valori prot
         {11, new List<ModificadorCarta>() }
        ,
         //energia counter
        {20, new List<ModificadorCarta>() }
        ,
        //modicicar valor counter
         {21, new List<ModificadorCarta>() }
        ,
    };

    //public int conter = 0;

    public GameManager gameManager;

    private void Start()
    {

        //Debug.Log(conter++);

        foreach(Card card in deck)
        {
            card.gm = gameManager;
            card.owner = this;
            card.rival = rival;
        }

        espacioslibres = new bool[espacios.Length];

        for (int i = 0; i < espacioslibres.Length; i++)
            espacioslibres[i] = true;

        nuevoTurno();
    }

    

    public void nuevoTurno()
    {
        //Debug.Log("Comienzo de turno");
        int contador = 0;
        foreach(bool espacio in espacioslibres)
        {
            //Debug.Log("Espacio " + contador + " = " + espacio);
            contador++;
            
            if (espacio)
                SacaCarta();
        }
    }

    public void SacaCarta()
    {
        
        //bool cointoss = new System.Random().Next(0, 2) == 0 ? true : false;

        //Debug.Log("Cointoss: " + cointoss); 

         Debug.Log("sacacarta");
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
                    carta.transform.SetSiblingIndex(i);
                    espacioslibres[i] = false;
                    deck.Remove(carta);
                    return;

                }

            }

        }
        else if (discard.Count >= 1) 
        { 
            /*
            deck.AddRange(discard); 
            discard.Clear();
            SacaCarta();
            */
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

            
            card.transform.position = espaciosJugado[jugada.IndexOf(card)].position;
            
            
            discard.Add(card);
           


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
