using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_Escalador : Controller_Player
{
    private bool enContactoConLaPared = false;
    private bool deslizandose = false;
    public float velocidadDeslizamiento;

    public float fuerzaSaltoParedX;
    public float fuerzaSaltoParedY;
    public float tiempoSaltoPared;

    private bool saltandoDePared;
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

            if (IsOnSomething() || enContactoConLaPared)
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
         
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            enContactoConLaPared = false;
        }
        base.OnCollisionExit(collision);
    }

    public override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Se apreto W");
            if (canJump)
            {
                if (enContactoConLaPared && deslizandose)
                {
                    saltoPared();
                }
                else
                {
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                }
                Debug.Log("Salto");
            }
        }
    }

    private void saltoPared() {
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft)
        {
            rb.AddForce(new Vector3(1 * -fuerzaSaltoParedX, fuerzaSaltoParedY, 0), ForceMode.Impulse);
        }else if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            rb.AddForce(new Vector3(1 * fuerzaSaltoParedX, fuerzaSaltoParedY, 0), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(0,fuerzaSaltoParedY,0), ForceMode.Impulse);
        }
        StartCoroutine(CambioSaltoPared());
    }

    IEnumerator CambioSaltoPared()
    {
        saltandoDePared = true;
        yield return new WaitForSeconds(tiempoSaltoPared);
        saltandoDePared = false;
    }
}
