using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    private Transform objetivo;

    public float velocidad = 70f, duracion = 3f;
    public int dano = 1;

//-------------------------------------------------------------------------------------------------------------------------
    public void Awake()
    {
        Destroy(gameObject, duracion);
    }
    public void Seek(Transform _objetivo)
    {
        objetivo = _objetivo;
    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = objetivo.position - transform.position;
        float distanciaPorFrame = velocidad * Time.deltaTime;

        transform.Translate(dir.normalized * distanciaPorFrame, Space.World);
        transform.LookAt(objetivo);

    }

//-------------------------------------------------------------------------------------------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HitTarget();
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
    void HitTarget()
    {
        Damage(objetivo);
        
        Destroy(gameObject);
    }

    void Damage(Transform jugador)
    {
        Jugador e = jugador.GetComponent<Jugador>();

        if (e != null)
        {
            e.RecibeDano(dano);
        }
    }
}
