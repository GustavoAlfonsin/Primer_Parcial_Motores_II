using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Loading;
using UnityEngine;

public class Controller_Plataformas : Controller_Obstaculos
{
    public GameObject[] waypoints;
    public float speed = 2;
    private int waypointActual = 0;

    // Update is called once per frame
    public override void Update()
    {
        if (obstaculoActivado)
        {
            moverPlataforma();
        }
        base.Update();
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
}
