using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public GameManager gameManager;
    //-------------------------------------
    public GameObject[] Enemigos;
    public float CooldownEnemigos = 5f;
    public float CooldownOleadas = 25f;

    public int ContadorDeEnemigos;

    public bool EnOleada = true;


    void Update()
    {
        if(EnOleada == true)
        {
            StartCoroutine(SpawnDeEnemigos());
        }
    }

    private IEnumerator SpawnDeEnemigos()
    {                                                                                   //Empieza la Oleada
        EnOleada = false;

        WaitForSeconds cooldownEnemigos = new WaitForSeconds(CooldownEnemigos);         //Determinamos el cooldown entre cada enemigo

        for (int i = 0; i < ContadorDeEnemigos; i++)                                    //Comienzan a aparecer hasta llegar al límite maximo
        {
            int aleatorio = Random.Range(0, Enemigos.Length);
            GameObject enemigoASpawnear = Enemigos[aleatorio];
            Instantiate(enemigoASpawnear, transform.position, Quaternion.identity);
            yield return cooldownEnemigos;
        }

        if(gameManager.EnemigosDerrotados == ContadorDeEnemigos)
        {
            ContadorDeEnemigos = ContadorDeEnemigos * 2;                                   //Determinamos la cantidad de enemigos de la siguiente oleada

            yield return new WaitForSeconds(CooldownOleadas);                              //Esperamos el cooldown de la siguiente oleada

            EnOleada = true;                                                               //Empieza la siguiente oleada
        }
                                                                     
    }
}
