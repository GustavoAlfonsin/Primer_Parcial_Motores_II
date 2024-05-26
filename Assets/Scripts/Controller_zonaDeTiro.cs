using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_zonaDeTiro : MonoBehaviour
{
    private GameObject rb;
    public static GameObject playerSercano;
    public bool JugadorEnZona;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<GameObject>();
        JugadorEnZona = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            JugadorEnZona = true;
            playerSercano = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            JugadorEnZona = false;
            playerSercano = null;
        }
    }
}
