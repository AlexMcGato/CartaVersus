using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;

    public Adversario foe;

    public List<Card> jugadaPlayer;

    public List<Card> jugadaAdversario;

    public String[] combatiente = new String[2] { "Jugador", "Adversario" };

    public bool[] prioridad = new bool[2] {false, false};

    public int playerAttack = 0;
    public int playerCounter = 0;
    public int playerProt = 0;

    public int foeAttack = 0;
    public int foeCounter = 0;
    public int foeProt = 0;

    public String ganador = "";

    public void resolver()
    {

        bool res_prio;

        if (prioridad[0] == prioridad[1])
            res_prio = new System.Random().Next(0, 2) == 0 ? true : false;
        else if (prioridad[0])
            res_prio = true;
        else
            res_prio = false;

            Debug.Log("Cointoss: " + res_prio);


        int playedCardCount = jugadaPlayer.Count >= jugadaAdversario.Count ? jugadaPlayer.Count : jugadaAdversario.Count ;

        Debug.Log("Inicio de resolucion");
        if (res_prio)
        {
            Debug.Log("Iniciativa del jugador");
            //Debug.Log(jugadaPlayer.Count);

             

            for (int i = 0; i < playedCardCount; i++)
            {              
               //carta recibe clash de la otra
               

                if (i <= jugadaAdversario.Count - 1)
                    jugadaAdversario[i].clash(jugadaPlayer[i]);

                if (i <= jugadaPlayer.Count - 1)
                    jugadaPlayer[i].clash(jugadaAdversario[i]);

                regularCombatTrigger();
            }
        }
        else
        {
            for (int i = 0; i < playedCardCount; i++)
            {
                Debug.Log("Iniciativa del adversario");

                if (i <= jugadaAdversario.Count - 1)
                    //jugadaAdversario[i].clash();

                if (i <= jugadaPlayer.Count - 1)
                    //jugadaPlayer[i].clash();

                regularCombatTrigger();
            }
        }

        Debug.Log("Final de resolucion");

    }

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
}


