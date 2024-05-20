using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller_Obstaculos : MonoBehaviour
{
    internal bool obstaculoActivado;
    public Rigidbody rb;
    public GameObject bton;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        obstaculoActivado = bton.GetComponent<Controller_Buttons>().activado;
    }

    
}
