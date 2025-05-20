using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;

    public Adversario foe;

    public List<Card> jugadaPlayer;

    public List<Card> jugadaAdversario;

    public String[] combatiente = new String[2] { "Jugador", "Adversario" };

    public int playerAttack = 0;
    public int playerCounter = 0;
    public int playerProt = 0;

    public int foeAttack = 0;
    public int foeCounter = 0;
    public int foeProt = 0;

    public String ganador = "";

    public void resolver()
    {

        Debug.Log("Inicio de resolucion");
        if (jugadaAdversario.Count <= jugadaPlayer.Count)
        {
            //Debug.Log("MainPlayer usa mismas o mas cartas");
            //Debug.Log(jugadaPlayer.Count);

            for (int i = 0; i < jugadaPlayer.Count; i++)
            {
                //Debug.Log("Activando efecto de carta");
                jugadaPlayer[i].efecto();

                if (i <= jugadaAdversario.Count - 1)
                    jugadaAdversario[i].efecto();

                regularCombatTrigger();
            }
        }
        else
        {
            for(int i = 0; i < jugadaAdversario.Count; i++)
            {

                jugadaAdversario[i].efecto();

                if (i <= jugadaPlayer.Count - 1)
                    jugadaPlayer[i].efecto();

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


