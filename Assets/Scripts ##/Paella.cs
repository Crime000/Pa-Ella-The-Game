using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paella : MonoBehaviour
{
    public GameManager gameManager;
    private Jugador player;
    public string Jugador = "Player";
    public float range;

//-------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        ComprarPaella();
    }

//-------------------------------------------------------------------------------------------------------------------------
    public void ComprarPaella()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(Jugador);
        float DistanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);
        player = jugador.GetComponent<Jugador>();

        if (DistanciaAlJugador <= range)
        {
            if (Input.GetKeyDown(KeyCode.F) && gameManager.Monedas >= 20)
            {
                gameManager.Monedas -= 20;
                player.vida += 5;
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
