using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_Escalador : Controller_Player
{
    private bool enContactoConLaPared = false;
    private bool deslizandose = false;
    public float velocidadDeslizamiento;
    private bool contactoParedIzq = false;
    private bool contanctoParedDer = false;
    // Start is called before the first frame update
    public override void Start()
    {
        //enContactoConLaPared = false;
        //deslizandose = false;
        //contactoParedIzq = false;
        //contanctoParedDer = false;

        base.Start();   
    }
    public override void FixedUpdate()
    {
        if (GameManager.actualPlayer == playerNumber)
        {
            if (deslizandose)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -velocidadDeslizamiento, float.MaxValue), rb.velocity.z);
            }
            else
            {
                Movement();
            }
            
        }
    }

    public override void Update()
    {
        if (GameManager.actualPlayer == playerNumber)
        {
            Jump();
            if (SomethingLeft())
            {
                canMoveLeft = false;
            }
            else
            {
                canMoveLeft = true;
            }
            if (SomethingRight())
            {
                canMoveRight = false;
            }
            else
            {
                canMoveRight = true;
            }

            if (IsOnSomething())
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
            Debug.Log($"{canJump}");
            if (enContactoConLaPared && !onFloor)
            {
                deslizandose = true;
            }
            else
            {
                deslizandose = false;
            }

        }
        else
        {
            if (onFloor) // Si no es un personaje activo y esta en el piso freza la posición
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                if (IsOnSomething()) // si esta sobre otro personaje freza la posición en Z
                {
                    if (downHit.collider.gameObject.CompareTag("Player"))
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            enContactoConLaPared = true;
            if (SomethingLeft())
            {
                contactoParedIzq = true;
                canMoveLeft = false;
            }
            else if (SomethingRight())
            {
                contanctoParedDer = true;
                canMoveRight = false;
            }
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            enContactoConLaPared = false;
            contactoParedIzq = false;
            contanctoParedDer = false;
        }
        base.OnCollisionExit(collision);
    }
    //internal override void Movement()
    //{
    //    if (Input.GetKey(KeyCode.A) && canMoveLeft)
    //    {
    //        rb.velocity = new Vector3(1 * -speed, rb.velocity.y, 0);
    //    }
    //    else if (Input.GetKey(KeyCode.D) && canMoveRight)
    //    {
    //        rb.velocity = new Vector3(1 * speed, rb.velocity.y, 0);
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    //    }
    //}

    public override void Jump()
    {
        base.Jump();
    }
}
