using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] Enemigos;
    public float CooldownEnemigos = 5f, CooldownOleadas = 25f;
    public int xPOS, zPOS, rango;

    public int ContadorDeEnemigos;

    public bool EnOleada = true;

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if(EnOleada == true && ContadorDeEnemigos <= 28)
        {
            StartCoroutine(SpawnDeEnemigos());
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
    private IEnumerator SpawnDeEnemigos()
    {                                                                                   //Empieza la Oleada
        EnOleada = false;

        WaitForSeconds cooldownEnemigos = new WaitForSeconds(CooldownEnemigos);         //Determinamos el cooldown entre cada enemigo

        for (int i = 0; i < ContadorDeEnemigos; i++)                                    //Comienzan a aparecer hasta llegar al límite maximo
        {
            xPOS = Random.Range(20, rango);
            zPOS = Random.Range(20, rango);
            int aleatorio = Random.Range(0, Enemigos.Length);
            GameObject enemigoASpawnear = Enemigos[aleatorio];
            Instantiate(enemigoASpawnear, new Vector3(xPOS, transform.position.y, zPOS), Quaternion.identity);
            yield return cooldownEnemigos;
        }

         ContadorDeEnemigos = ContadorDeEnemigos * 2;                                   //Determinamos la cantidad de enemigos de la siguiente oleada

         yield return new WaitForSeconds(CooldownOleadas);                              //Esperamos el cooldown de la siguiente oleada

         EnOleada = true;                                                               //Empieza la siguiente oleada
        
    }

//-------------------------------------------------------------------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
