using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Disparador : MonoBehaviour
{
    // Se trata de un objeto que cuando tiene a alguno de los jugadores serca le dispare
    public float shootCoolDown;
    public GameObject bala;
    public GameObject salidaDisparo;
    
    public GameObject AreaDisparo;
    // Start is called before the first frame update
    void Start()
    {
        shootCoolDown = UnityEngine.Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        shootCoolDown -= Time.deltaTime;
        shoot();
    }

    private void shoot() {
        if (AreaDisparo.GetComponent<Controller_zonaDeTiro>().JugadorEnZona)
        {
            if(shootCoolDown <= 0)
            {
                Instantiate(bala,salidaDisparo.transform.position, Quaternion.identity);
                Debug.Log("Salio un disparo");
                shootCoolDown = UnityEngine.Random.Range(1, 6);
            }
        }
    }
}
