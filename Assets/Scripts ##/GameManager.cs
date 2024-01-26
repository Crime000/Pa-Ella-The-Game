using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int MonedasTotales { get { return Monedas; } }

    public int Monedas;
    public int EnemigosDerrotados;
    public float tiempo;
    public float Oleadas = 0;
    public float CooldownOleadas = 25;
    public bool EnOleada = false;

    public void Update()
    {
        tiempo += Time.deltaTime;
        ControlDeOleadas();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Mas de un Game Manager");
        }
    }

    public void ControlDeOleadas()
    {
        if (Oleadas == 0 && EnOleada == false)
        {
            EnOleada = true;
            Oleadas = 1f;
        }
        else if (Oleadas == 1 && EnOleada == true && EnemigosDerrotados == 7)
        {
            EnOleada = false;
            CooldownOleadas -= (tiempo += Time.deltaTime);

            Oleadas = 2;
        }
        if (Oleadas == 2 && EnOleada == false)
        {
            EnOleada = true;
        }
        else if (Oleadas == 2 && EnOleada == true && EnemigosDerrotados == 21)
        {
            EnOleada = false;
            CooldownOleadas -= (tiempo += Time.deltaTime);

            Oleadas = 3;
        }
        if (Oleadas == 3 && EnOleada == false)
        {
            EnOleada = true;
        }
        else if (Oleadas == 3 && EnOleada == true && EnemigosDerrotados == 49)
        {
            EnOleada = false;
            Debug.Log("El banco se ha salvado");
        }
    }

    public void MonedasPorEnemigo(int MonedasObtenidos)
    {
        Monedas += MonedasObtenidos;
    }
}
