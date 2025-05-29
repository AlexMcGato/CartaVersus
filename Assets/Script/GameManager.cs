using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;

    public Adversario rival;

    public List<Card> jugadaPlayer;

    public List<Card> jugadaAdversario;

    public MonoBehaviour mensajeFinal;
    public TextMeshProUGUI mensajeFinalTexto;

    public Card emptyCardJugador;
    public Card emptyCardAdversario;

    public String[] combatiente = new String[2] { "Jugador", "Adversario" };

    public bool[] prioridad = new bool[2] {false, false};

    public bool playerHaJugado = false;
    public bool rivalHaJugado = false;

    public bool finPartida = false;

    public String ganador = "";



    
    public void resolver()
    {
        if (!playerHaJugado || !rivalHaJugado)
        {
            return;
        }
        else
            StartCoroutine(ejecutarResolucion());

        
       
    }

    IEnumerator ejecutarResolucion() 
    {
        yield return new WaitForSeconds(1);

        playerHaJugado = false;
        rivalHaJugado = false;

        bool res_prio;

        if (prioridad[0] == prioridad[1])
            res_prio = new System.Random().Next(0, 2) == 0 ? true : false;
        else if (prioridad[0])
            res_prio = true;
        else
            res_prio = false;

        Debug.Log("Cointoss: " + res_prio);


        //sacar numero mas alto de cartas juagadas
        int playedCardCount = jugadaPlayer.Count >= jugadaAdversario.Count ? jugadaPlayer.Count : jugadaAdversario.Count;

        //Creo cartas de efecto vacio para tener un objetivo en caso de que un bando juege mas cartas que el otro

        Card genericaJugador = emptyCardJugador;
        Card genericaAdversario = emptyCardAdversario;

        Debug.Log("Inicio de resolucion");
        if (res_prio)
        {
            Debug.Log("Iniciativa del jugador");
            //Debug.Log(jugadaPlayer.Count);


            for (int i = 0; i < playedCardCount; i++)
            {
                nextCard();
                //carta recibe clash de la otra


                if (i < jugadaAdversario.Count)
                {
                    //no se sabe aun si el jugador sigue teniendo cartas en juego o no
                    if (i < jugadaPlayer.Count)
                        jugadaAdversario[i].clash(jugadaPlayer[i]);
                    else
                        jugadaAdversario[i].clash(genericaJugador);
                }
                else
                {
                    //si seguimos aqui y al adversario no le quedan cartas, quiere decir que el jugador ha usado mas y playedcardcount es las que ha usado
                    genericaAdversario.emptyClash(jugadaPlayer[i]);
                }


                if (finPartida)
                    StopCoroutine(ejecutarResolucion());

                //regularCombatTrigger();
            }
        }
        else
        {
            for (int i = 0; i < playedCardCount; i++)
            {
                nextCard();
                //carta recibe clash de la otra
                Debug.Log("Iniciativa del adversario");

                if (i < jugadaPlayer.Count)
                {
                    //no se sabe aun si el adversario sigue teniendo cartas en juego o no
                    if (i < jugadaAdversario.Count)
                        jugadaPlayer[i].clash(jugadaAdversario[i]);
                    else
                        jugadaPlayer[i].clash(genericaAdversario);
                }
                else
                {
                    //si seguimos aqui y al adversario no le quedan cartas, quiere decir que el adversario ha usado mas
                    genericaJugador.emptyClash(jugadaAdversario[i]);
                }


                if (finPartida)
                    StopCoroutine(ejecutarResolucion());

                //regularCombatTrigger();
            }
        }


        Debug.Log("Final de resolucion");

        
        StartCoroutine(delayTurno());
    }

    IEnumerator delayTurno()
    {
        yield return new WaitForSeconds(2);
        nuevoturno();
    }
    //reset de valores de por carta (proteccion)
    public void nextCard()
    {
        player.nextCardResolution();
        rival.nextCardResolution();
    }
    public void nuevoturno()
    {
        if (!finPartida)
        {
            player.nuevoTurno();
            rival.nuevoTurno();
        }
        
    }

    public void finDePartida(String mensaje)
    {
        finPartida = true;
        mensajeFinalTexto.text = mensaje;
        mensajeFinal.gameObject.SetActive(true);
    }
    //metodos obsoleto con el nuevo sistema de efectos
    /*
    public void regularCombatTrigger()
    {
        if (playerAttack <= 0 && foeAttack <= 0)
        {
            Debug.Log("Ningun bando ataca");

            resetCombatValues();
            return;
        }
           
        if (playerCounter != 0 && foeAttack >0)
        {
            Debug.Log("player counter");

            foe.damage(foeAttack + playerCounter - foeProt);
            resetCombatValues();
            return;
        }

        if (foeCounter != 0 && playerAttack >0)
        {
            Debug.Log("foe counter");

            player.damage(playerAttack + foeCounter -  playerProt);
            resetCombatValues();
            return;
        }
        
        if (playerAttack > 0)
        {
            Debug.Log("player attacks");
            foe.damage(playerAttack - foeProt);
        }
            

        if (foeAttack > 0) 
        {
            Debug.Log("foe attacks");
            player.damage(foeAttack - playerProt);
        }

        resetCombatValues();

    }

    private void resetCombatValues()
    {
        foeAttack = 0;
        foeCounter = 0;
        foeProt = 0;
        playerAttack = 0;
        playerCounter = 0;
        playerProt = 0;
    }

    internal void modifierToFoe(ModificadorCarta modifier)
    {
        foe.cardMods[modifier.category].Add(modifier);
    }
    */
}


