using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPrincipal : MonoBehaviour
{
    public GameObject player;
    private Vector3 offsetCamara;

    public float offSetY, offSetZ;
    public float Suavizado;
    public float sensibilidad = 1f;

    private void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offSetY, player.transform.position.z - offSetZ);
        offsetCamara = transform.position - player.transform.position;
    }
    void Update()
    {
         transform.LookAt(player.transform.position);

         Quaternion giroCamara = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up);
         offsetCamara = giroCamara * offsetCamara;
    }
}
