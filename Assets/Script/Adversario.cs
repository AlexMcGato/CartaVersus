using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversario : Player
{
    

    
    public override void damage(int damage)
    {
        
        base.damage(damage);

        if (hp <= 0)
        {
            gameManager.ganador = gameManager.combatiente[0];
            gameManager.finDePartida("¡Victoria!");
        }
    }
    public override void getCountered(int damage)
    {
        base.getCountered(damage);

        if (hp <= 0)
        {
            gameManager.ganador = gameManager.combatiente[0];
            gameManager.finDePartida("¡Victoria!");
        }

    }

    public override void nuevoTurno()
    {
        base.nuevoTurno();

        //a continuacion toda la logica para meter cartas en la siguiente jugada
        //Debug.Log("(0)Inicio espera para seleccion");
        StartCoroutine(delaySeleccion());

        StartCoroutine(delayJugada());
        
        
    }

    IEnumerator delaySeleccion()
    {
        //Debug.Log("(1)Inicio espera para seleccion");
        yield return new WaitForSeconds(.5f);
        selectJugada();
    }
    IEnumerator delayJugada()
    {
        yield return new WaitForSeconds(1f);
        jugarMano();
    }
    public void selectJugada()
    {
        Debug.Log("Comienza seleccion de jugada");
        bool controlador = false;
        int controlObservadas = 0;
        int controlAleatorio = 0;
        int cartasNoUsables = 0;
        List<Card> copiaMano = new List<Card>(mano);

        for (int i = 0; i < 4; i++)
        {
            //solo buscaremos cartas si existe alguna que se pueda usar
            if (copiaMano[i].coste < mana)
                controlador = true;

        }

        //contador plano para arreglar error de bucle infinito
        int hardcounter = 0;

        while (controlador)
        {
            //Debug.Log("Hardcounter = " + hardcounter);

            //Debug.Log("ControlAleatorio(principio) =" + controlAleatorio);

            int aleatorio = UnityEngine.Random.Range(controlAleatorio, 4);

            //Debug.Log("Aleatorio = " + aleatorio);

            //saca una aleatoria de las primeras 4 (0-3)
            //como las cartas de la mano se ponen las primeras, son esas 4
            //esto NO va a funcionar, una cosa es el orden como sibling y otra el orden en la lista
            //el de sibling lo quiero SOLO para ordenarlo visualmente
            Card carta = copiaMano[aleatorio];

            //Debug.Log("Carta seleccionada = " + carta.name);
            //si la carta cuesta mas mana del que se puede gastar, se pone arriba y se aumenta el contador de cartas no usables para no volver a mirarla
            if (this.mana <= carta.coste)
            {

                hardcounter++;
                cartasNoUsables++;
                copiaMano.RemoveAt(aleatorio);
                copiaMano.Insert(0, carta);
                //carta.transform.SetSiblingIndex(0);
                controlAleatorio = controlObservadas + cartasNoUsables;
                //Debug.Log("Cartas no usables= " + cartasNoUsables);
                continue;
            }


            if (!jugada.Contains(carta) && this.mana > carta.coste)
            {
                if (carta.efectocarta is not ProtEfect && carta.efectocarta is not CounterEfect)
                {
                    if (controlObservadas < 4)
                    {
                        if ((this.mana - (carta.coste + manaSpendPreview)) >= 4)
                        {
                            Debug.Log("(0)Carta sumada desde posicion "+ mano.IndexOf(carta));
                            jugada.Add(carta);
                            
                            espacioslibres[mano.IndexOf(carta)] = true;

                            manaSpendPreview += carta.coste;
                        }

                    }
                    else if (jugada.Count < 2 && (this.mana - (carta.coste + manaSpendPreview)) > 0)
                    {
                        Debug.Log("(1)Carta sumada desde posicion " + mano.IndexOf(carta));
                        jugada.Add(carta);
                        espacioslibres[mano.IndexOf(carta)] = true;
                        manaSpendPreview += carta.coste;
                    }


                }
                else
                {
                    if ((this.mana - (carta.coste + manaSpendPreview)) > 0)
                    {
                        Debug.Log("(2)Carta sumada desde posicion " + mano.IndexOf(carta));
                        jugada.Add(carta);
                        espacioslibres[mano.IndexOf(carta)] = true;
                        manaSpendPreview += carta.coste;
                    }


                }
                //pongo la carta en rango 0 y luego usare el control de las observadas para excluir los rangos bajos del aleatorio
                //Debug.Log("ControlObservadas= " + controlObservadas);
                controlObservadas++;

            }

            //una vez haya llegado a ver las 4 cartas, se reducen los requisitos para jugar por lo menos 2 cartas 
            if (controlObservadas < 4)
            {
                //si ya se han mirado las 4 da igual donde esten, y mejor no andar mareandolas por lo que pueda pasar
                copiaMano.RemoveAt(aleatorio);
                copiaMano.Insert(0, carta);
                controlAleatorio = controlObservadas + cartasNoUsables;
                //Debug.Log("ControlAleatorio(fin) = " + controlAleatorio);
            }
            else
            {
                //si ya se han mirado las 4 cartas de la mano, el aleatorio sera entre todas las que se puedan usar
                controlAleatorio = cartasNoUsables;
            }

            //solo le voy a dejar que mire un total de 12 cartas( 4 unicas la primera vuelta + 8 aleatorio repetible) para asegurarme de que no se haga un bucle infinito
            if (jugada.Count >= 4 || (jugada.Count >= 2 && controlObservadas >= 4) || controlObservadas >= 12)
            {
                controlador = false;
            }

            hardcounter++;




        }
        /*
       for(int i = 0; i < mano.Count; i++)
       {
           Debug.Log("Carta en pos " + i + " es " + mano[i].name);
       }
        /*checkeo por el bucle infinito*/
    }
    public override void jugarMano()
    {
        base.jugarMano();
        gameManager.jugadaAdversario = jugada;
        gameManager.rivalHaJugado = true;
        gameManager.resolver();
    }

    

    //metodos obsoletos por el sistema de efectos
    /*
    public override void attack(int value)
    {
        gameManager.foeAttack = value;
    }
    public override void protect(int value)
    {
        gameManager.foeProt = value;
    }
    public override void counter(int value)
    {
        gameManager.foeCounter = value;
    }
    */
}
