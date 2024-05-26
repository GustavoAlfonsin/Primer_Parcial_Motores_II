using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_zonaDeTiro : MonoBehaviour
{
    private GameObject rb;
    public GameObject playerSecano;
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
            playerSecano = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            JugadorEnZona = false;
            playerSecano = null;
        }
    }
}
