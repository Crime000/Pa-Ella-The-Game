using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Torretas : MonoBehaviour
{
    private Transform objetivo;
    private Jugador player;
    public float range = 10f;
    public float velocidadRotacion;

    public GameObject balaPrefab;
    public float tiempoDisparos = 1f;
    private float ConteoDisparos = 0f;
    public Transform puntoFuego;

    public string Jugador = "Player";

//-------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1.5f);
    }

//-------------------------------------------------------------------------------------------------------------------------
    void UpdateTarget()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(Jugador);
        float DistanciaMasCorta = Mathf.Infinity;
        GameObject JugadorEnRango = null;
        
            float DistanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);
            if (DistanciaAlJugador < DistanciaMasCorta)
            {
                DistanciaMasCorta = DistanciaAlJugador;
                JugadorEnRango = jugador;
            }
        

        if (JugadorEnRango != null && DistanciaMasCorta <= range)
        {
            objetivo = JugadorEnRango.transform;
            player = JugadorEnRango.GetComponent<Jugador>();
        }
        else
        {
            objetivo = null;
        }

    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if (objetivo == null)
        {
            return;
        }

        LockOnTarget();

        if (ConteoDisparos <= 0f)
        {
            Shoot();
            ConteoDisparos = 1f / tiempoDisparos;
        }

            ConteoDisparos -= Time.deltaTime;
        
    }

//-------------------------------------------------------------------------------------------------------------------------
    void LockOnTarget()
    {
        Vector3 dir = objetivo.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * velocidadRotacion).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject BalaGO = (GameObject)Instantiate(balaPrefab, puntoFuego.position, puntoFuego.rotation);
        Bala bala = BalaGO.GetComponent<Bala>();

        if (bala != null)
            bala.Seek(objetivo);
    }

//-------------------------------------------------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
