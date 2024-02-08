using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    Rigidbody fisicas;
    public Transform jugadorTransform;
    public GameObject prefabCirculo;
    public string Banco = "Banco", Enemigo = "Enemy";
    public float velocidadBoost, velocidadNormal, velocidadActual, velocidadRapida, velRotacion, fuerzaSalto, vida, vidaInicial = 25, radioDelCirculo = 5f;
    public int mana, manaInicial = 3, cooldownBoost = 5;
    public bool PuedeSaltar = false, EnSuelo = false, saltarSi = false;

//-------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
        velocidadActual = velocidadNormal;
        vida = vidaInicial;
        mana = manaInicial;
    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()                                                              //---Rotación                 
    {
        if (Input.GetKey(KeyCode.A))
        {
            jugadorTransform.Rotate(0, -velRotacion * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            jugadorTransform.Rotate(0, velRotacion * Time.deltaTime, 0);
        }

 //-------------------------------------------------------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Q) && mana >= 1)                         //---Activar Habilidades
        {
            StartCoroutine(BoostVelocidad());
        }
        if (Input.GetKeyDown(KeyCode.E) && mana >= 1)
        {
            StartCoroutine(BoostAtaque());
        }
        if (Input.GetKeyDown(KeyCode.R) && mana >= 1)
        {
            Ultimate();
        }

//---------------------------------------------------------------------------------------------------------------------------    

        if (Input.GetButtonDown("Jump") && PuedeSaltar == true)                //---Activar Salto y Sprint
        {
            saltarSi = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual = velocidadRapida;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidadActual = velocidadNormal;
        }

    }
//-------------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate()                                                    //---Movimiento del jugador
    {
        if (Input.GetKey(KeyCode.W) && EnSuelo == true)
        {
            fisicas.velocity = transform.forward * velocidadActual * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && EnSuelo == true)
        {
            fisicas.velocity = -transform.forward * velocidadActual * Time.deltaTime;
        }
    }
//-------------------------------------------------------------------------------------------------------------------------
    public IEnumerator BoostVelocidad()                                           //---Habilidades
    {
        mana -= 1;
        int i = 0;
        while (i < 5)
        {
            velocidadActual = velocidadBoost;
            yield return new WaitForSeconds(1);
            i++;
        }
        velocidadActual = velocidadNormal;
    }

    public IEnumerator BoostAtaque()
    {
        GameObject enemigo = GameObject.FindGameObjectWithTag(Enemigo);
        Enemigos enemy = enemigo.GetComponent<Enemigos>();
        mana -= 1;
        int i = 0;
        while (i < 5)
        {
            enemy.dañoRecibido = 2;
            yield return new WaitForSeconds(1);
            i++;
        }
        enemy.dañoRecibido = 1;
    }

    public void Ultimate()
    {
        mana -= 1;
        GameObject banco = GameObject.FindGameObjectWithTag(Banco);
        Vector3 posUlti = new Vector3(banco.transform.position.x, 0, banco.transform.position.z);
        GameObject circulo = Instantiate(prefabCirculo, posUlti, Quaternion.identity);
        Destroy(circulo, 3f);

        Collider[] enemigosEnCirculo = Physics.OverlapSphere(posUlti, radioDelCirculo);

        foreach (Collider enemigoEnCirculo in enemigosEnCirculo)
        {
            if (enemigoEnCirculo.CompareTag("Enemy"))
            {
                Destroy(enemigoEnCirculo.gameObject);
            }
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
    public void RecibeDano(float cantidad)                                        //---Recibir daño y morir
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        Application.Quit();
        Destroy(gameObject);
    }

//-------------------------------------------------------------------------------------------------------------------------
    private void OnCollisionStay(Collision collision)                              //Salto del jugador
    {
        if (saltarSi && collision.gameObject.CompareTag("Terrain"))
        {
            fisicas.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            saltarSi = false;
            PuedeSaltar = false;
            EnSuelo = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") && PuedeSaltar == false)
        {
            PuedeSaltar = true;
            EnSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            PuedeSaltar = false;
            EnSuelo = false;
        }
    }
}
