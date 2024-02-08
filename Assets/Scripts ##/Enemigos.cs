using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{   
    public GameManager gameManager;
    private Banco bancoGO;
    public Animator anim;
    public string Banco = "Banco";
    public int Monedas, da�oRecibido;
    public float minVelocidad, maxVelocidad, velocidad;
    private int vidas = 4, da�o = 1, cooldownAtaque = 5;
    public bool atacando = false;

//-------------------------------------------------------------------------------------------------------------------------
    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();                             //--- Buscar el GameManager
 
        velocidad = Random.Range(minVelocidad, maxVelocidad) * Time.deltaTime;     //--- Velocidad Enemigo.
    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        GameObject banco = GameObject.FindGameObjectWithTag(Banco);
        float DistanciaAlBanco = Vector3.Distance(transform.position, banco.transform.position);
        bancoGO = banco.GetComponent<Banco>();

        if (atacando == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, banco.transform.position, velocidad);      //---Persecuci�n del Banco.

            if (DistanciaAlBanco <= bancoGO.rango)
            {
                StartCoroutine(AtacandoElBanco());
            }
        }

        if(DistanciaAlBanco > bancoGO.rango)
        {
            atacando = false;
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
    private IEnumerator AtacandoElBanco()                                            //---Atacar el Banco
    {
        atacando = true;
        for(int i = 0; i < 50; i++)
        {
            bancoGO.Vida -= da�o;
            
            yield return new WaitForSeconds(cooldownAtaque);
        }
        
    }

//-------------------------------------------------------------------------------------------------------------------------
    private void OnTrigerEnter(Collision other)
    {
        if (other.gameObject.tag == "Arma")                                          //---Recibir Da�o
        {
            if(anim != null)
            {
                anim.Play("Animaci�nAtaque");
            }

            vidas -= da�oRecibido;
        }

        if (vidas < 1)
        {
            gameManager.MonedasPorEnemigo(Monedas);

            Destroy(gameObject);
        }
    }
}
