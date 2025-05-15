using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public Transform[] espacios;
    public bool[] espacioslibres;


    public void SacaCarta()
    {
        Debug.Log("sacacarta");
        Console.WriteLine(deck.Count);
        if (deck.Count >= 1) {

            Debug.Log("sacacarta");
            Console.WriteLine(deck.Count);

            Card carta = deck[UnityEngine.Random.Range(0, deck.Count)];

            for (int i = 0; i<espacioslibres.Length; i++)
            {
                if (espacioslibres[i] == true) {
                    carta.gameObject.SetActive(true);
                    carta.transform.position = espacios[i].position;
                    espacioslibres[i] = false;
                    deck.Remove(carta);
                    return;

                }

            }

        }
    }
}


