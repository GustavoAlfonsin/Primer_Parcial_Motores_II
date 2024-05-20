using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Loading;
using UnityEngine;

public class Controller_Plataformas : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed = 2;
    private int waypointActual = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moverPlataforma();
    }

    void moverPlataforma()
    {
        if (Vector3.Distance(transform.position, waypoints[waypointActual].transform.position) < 0.1f)
        {
            waypointActual++;
            if (waypointActual >= waypoints.Length)
            {
                waypointActual = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointActual].transform.position,speed * Time.deltaTime );
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
