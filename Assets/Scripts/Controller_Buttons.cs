using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Buttons : MonoBehaviour
{
    public Material colorActivado;
    public Material colorDesactivado;

    public Rigidbody rb;

    internal bool activado;

   // public List<GameObject> Obstaculos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        activado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activado)
        {
            rb.GetComponent<MeshRenderer>().material = colorActivado;
        }
        else
        {
            rb.GetComponent<MeshRenderer>().material = colorDesactivado;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activado = !activado;
        }
    }
}
