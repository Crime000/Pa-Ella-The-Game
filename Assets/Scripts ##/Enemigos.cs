using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{   
    
    public GameManager gameManager;
    public Transform objetivo;
    public int Monedas;
    public float minVelocidad;
    public float maxVelocidad;
    float velocidad;
    int vidas = 4;
    int daño = 1;
    public bool atacando = false;


    // Start is called before the first frame update
    void Start()
    {
        // Posición Enemigo.
        //transform.position = new Vector3(1,0.5f,1);  //Cambiar cuando tengamos los puntos de spawn
        // Velocidad Enemigo.
        velocidad = Random.Range(minVelocidad, maxVelocidad) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(atacando == false)
        {
            // Persecución del Banco.
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad);

        }
        else
        {
            AtacandoElBanco();
        }
        
    }


    private void AtacandoElBanco()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Banco"))
        {
            atacando = true;
        }


        if (other.gameObject.CompareTag("Player")) //luego debe ser el arma
        {
            vidas -= 1;

            if(vidas < 1)
            {
                gameManager.MonedasPorEnemigo(Monedas);
            
                Destroy(gameObject);
            } 
        }
    }
}
