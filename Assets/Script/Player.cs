using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string nombre = "";
    public Player rival;

    public bool canPlay = true;

    public int defensa = 1;

    public int mana = 10;
    public int maxMana = 10;
    public int baseMana = 10;
    public int manaRegen = 3;
    public Slider manaBar;
    public Slider manaSpendBar;

    public int hp = 3;
    public int prot = 0;
    public bool losthp =false;

    public TextMeshProUGUI cantidadDescartes;
    public TextMeshProUGUI cantidadMazo;
    public TextMeshProUGUI defensaUI;

    public Image[] hpMarkers;
  

    public int manaSpendPreview = 0;
    
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();
    public List<Card> mano = new List<Card>();

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
        manaBar.maxValue = maxMana;
        manaSpendBar.maxValue = maxMana;
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

    

    public virtual void nuevoTurno()
    {
        canPlay = true;
        //Debug.Log("Comienzo de turno");
        //int contador = 0;
        prot = 0;
        if (losthp)
        {
            this.mana = (maxMana / 2)-1;
            losthp = false;
        }

        this.mana += manaRegen;

        foreach (Card card in jugada)
        {
            discard.Add(card);
            card.transform.position = discardSpace.position;
            card.gameObject.SetActive(false);
            
        }

        jugada.Clear();

        foreach(bool espacio in espacioslibres)
        {
            //Debug.Log("Espacio " + contador + " = " + espacio);
            //contador++;
            
            if (espacio)
                SacaCarta();
        }
        cantidadDescartes.text = discard.Count.ToString();
        cantidadMazo.text = deck.Count.ToString();
        defensaUI.text = defensa.ToString();
        updateMana();
    }

    public void updateMana()
    {
        manaBar.value = mana;
        manaSpendBar.value = manaSpendPreview;


    }
    public void SacaCarta()
    {
        
        //bool cointoss = new System.Random().Next(0, 2) == 0 ? true : false;

        //Debug.Log("Cointoss: " + cointoss); 

         //Debug.Log("sacacarta");
        // Console.WriteLine(deck.Count);
        if (deck.Count >= 1)
        {

            // Debug.Log("sacacarta");
            // Console.WriteLine(deck.Count);

            Card carta = deck[UnityEngine.Random.Range(0, deck.Count)];


            for (int i = 0; i < espacioslibres.Length; i++)
            {
                if (espacioslibres[i])
                {
                    Debug.Log("Carta sacada");
                    carta.seleccionable = true;

                    carta.gameObject.SetActive(true);
                    carta.transform.position = espacios[i].position;
                    carta.transform.SetSiblingIndex(i);
                    espacioslibres[i] = false;
                    mano.Insert(i,carta);
                    deck.Remove(carta);
                    return;

                }

            }

        }
        else if (discard.Count >= 1) 
        { 
            
            deck.AddRange(discard); 
            discard.Clear();
            SacaCarta();
            
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
          prot += value;
    }

    public List<Card> aplicarModificadores(List<Card> cartas)
    {
        List<Card> resultCards = new List<Card>();
        foreach (Card card in cartas)
        {

            //reset de carrtas por si se ha acabado el efecto
            //(solo bajo un nivel en los adicionales, no deberia haber mas pero podria bajar mas si fuera necesario)
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
                    foreach (ModificadorCarta modifier in entry.Value)
                    {

                        card.efectocarta.modifyEfect(modifier.value);
                        modifier.applyMod();
                    }
                }

                //efectos secundarios
                foreach (Efecto secundario in card.efectocarta.adicionales)
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


          resultCards.Add(card);  


        }

        return resultCards; 
    }

    public virtual void jugarMano()
    {
        List<Card> resultCards = new List<Card>();

        jugada = aplicarModificadores(jugada);
        foreach(Card card in jugada)
        {
            card.transform.position = espaciosJugado[jugada.IndexOf(card)].position;
            Debug.Log("carta jugada" + jugada.IndexOf(card));
            card.seleccionable = false;
            mano.Remove(card);
        }
        
        this.mana -= manaSpendPreview;
        this.manaSpendPreview = 0;
        updateMana();
        canPlay = false;

    }

    public void nextCardResolution()
    {
        //this.prot = 0;
    }
    
    public virtual void getCountered(int damage)
    {
        Debug.Log(nombre + " recibe " + damage + " puntos de counter dmg con " + defensa + " defensa ");
        int incomingDMG = damage - (defensa);

        Debug.Log("Mana previo " + mana);
        mana -= incomingDMG >= 0 ? incomingDMG : 0;
        Debug.Log("Mana posterior " + mana);


        if (mana <= 0 && incomingDMG>0)
        {
            mana = 0;
            hp--;
            losthp = true;
            //marcadores van 0-2 es facil desactivar el ultimo sin mas
            hpMarkers[hp].gameObject.SetActive(false);
        }
    }

    public virtual void damage(int damage)
    {
        Debug.Log(nombre + " recibe "+ damage + " puntos de dmg con "+defensa +" defensa y "+ prot +" protect");
        int incomingDMG = damage - (defensa + prot);

        Debug.Log("Mana previo " + mana);
        mana -= incomingDMG >= 0 ? incomingDMG : 0;
        Debug.Log("Mana posterior " + mana);

        if (prot > 0)
            prot--;


        if (mana <= 0 && incomingDMG > 0)
        {
            mana = 0;
            hp--;
            losthp = true;
            //marcadores van 0-2 es facil desactivar el ultimo sin mas
            if(hpMarkers.Length > 0) 
            hpMarkers[hp].gameObject.SetActive(false);

        }
    }

    public virtual void giveModifier(ModificadorCarta weaken)
    {
       
    }
}
