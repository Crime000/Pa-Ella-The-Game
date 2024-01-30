using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float movX, movZ;
    Rigidbody fisicas;
    public float velocidad;
    public float velocidadRapida;
    public float velRotacion;
    public bool saltarSi = false;
    public float fuerzaSalto;

    // Start is called before the first frame update
    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            saltarSi = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = velocidadRapida;
        }
        else
        {
            velocidad = 5;
        }

    }

    private void FixedUpdate()
    {
        Vector3 Velocidad = new Vector3(movX * velocidad, fisicas.velocity.y, movZ * velocidad);
        fisicas.velocity = Velocidad;

        if (Velocidad != Vector3.zero)
        {
            Quaternion Rotacion = Quaternion.LookRotation(Velocidad, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotacion, velRotacion * Time.deltaTime);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (saltarSi && collision.gameObject.CompareTag("Terrain"))
        {
            fisicas.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            saltarSi = false;
        }
    }
}
