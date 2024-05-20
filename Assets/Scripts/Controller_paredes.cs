using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_paredes : Controller_Obstaculos
{
    public Vector3 posicionInc;
    public GameObject posicionFin;

    public override void Start()
    {
        base.Start();
        posicionInc = rb.position;
    }

    public override void Update()
    {
        abrirPuerta();
        base.Update();
    }

    private void abrirPuerta()
    {
        if (!obstaculoActivado)
        {
            rb.transform.position = posicionInc;
        }
        else
        {
            rb.transform.position = posicionFin.transform.position;
        }
    }
}
