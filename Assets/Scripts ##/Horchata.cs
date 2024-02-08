using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horchata : MonoBehaviour
{

    public GameManager gameManager;
    private Jugador player;
    public string Jugador = "Player";
    public float range;

//-------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        ComprarHorchata();
    }

//-------------------------------------------------------------------------------------------------------------------------
    public void ComprarHorchata()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(Jugador);
        float DistanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);
        player = jugador.GetComponent<Jugador>();

        if (DistanciaAlJugador <= range)
        {
            if (Input.GetKeyDown(KeyCode.F) && gameManager.Monedas >= 10)
            {
                gameManager.Monedas -= 10;
                player.mana += 1;
            }
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
