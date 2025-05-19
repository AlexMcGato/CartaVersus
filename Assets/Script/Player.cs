using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public int defensa = 1;
    public int stamina = 10;
    public int hp = 3;
    
    public List<Card> deck = new List<Card>();
    public Transform[] espacios;
    public bool[] espacioslibres;

    public List<Card> jugada = new List<Card>();

    public GameManager gameManager;

    private void Start()
    {
        espacioslibres = new bool[espacios.Length];

        for (int i = 0; i < espacioslibres.Length; i++)
            espacioslibres[i] = true;
    }

    public void SacaCarta()
    {
       // Debug.Log("sacacarta");
       // Console.WriteLine(deck.Count);
        if (deck.Count >= 1)
        {

           // Debug.Log("sacacarta");
           // Console.WriteLine(deck.Count);

            Card carta = deck[UnityEngine.Random.Range(0, deck.Count)];
             carta.player = this;

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

    public virtual void jugarMano()
    {
       
    }

    public virtual void damage(int damage)
    {
       
    }
}
