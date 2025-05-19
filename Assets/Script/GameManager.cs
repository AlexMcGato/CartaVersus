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
        foreach (Card card in jugadaPlayer)
        {
            card.gm = this;
        }

        if (jugadaAdversario.Count <= jugadaPlayer.Count)
        {


            for (int i = 0; i < jugadaPlayer.Count; i++)
            {

                jugadaPlayer[i].efecto();

                if (i <= jugadaAdversario.Count - 1)
                    jugadaAdversario[i].efecto();

                triggerCard();
            }
        }
        else
        {
            for(int i = 0; i < jugadaAdversario.Count; i++)
            {

                jugadaAdversario[i].efecto();

                if (i <= jugadaPlayer.Count - 1)
                    jugadaPlayer[i].efecto();

                triggerCard();
            }
        }

        

    }

    public void triggerCard()
    {
        if (playerCounter != 0 && foeCounter != 0)
            return;


        if (playerCounter != 0)
        {
           if (foeProt == 0) 
                foe.damage(foeAttack + playerCounter);
        }

        if (foeCounter != 0)
        {
            if (playerProt == 0)
                player.damage(playerAttack + foeCounter);
        }
        


    }
    
}


