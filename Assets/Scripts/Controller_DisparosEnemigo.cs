using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_DisparosEnemigo : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction;

    private Rigidbody rb;

    public float velocidadDisparos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Controller_zonaDeTiro>().playerSecano.gameObject;
        direction = -(this.transform.position - player.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(rb.position,direction, velocidadDisparos * Time.deltaTime);
        rb.AddForce(direction * velocidadDisparos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Water"))
        {
            Destroy(this.gameObject);
        }
    }
}
