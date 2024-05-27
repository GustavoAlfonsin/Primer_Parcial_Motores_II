using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player_Escalador : Controller_Player
{
    // La idea es hacer un nuevo personaje que puede estar en la pared para que salte y pueda subir por ellas
    [Header("Salto Pared")]
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
            if (deslizandose && !IsOnSomething())
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -velocidadDeslizamiento, float.MaxValue), 0);
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
            if (SomethingLeft() && !leftHit.collider.CompareTag("Pared"))
            {
                canMoveLeft = false;
            }
            else
            {
                canMoveLeft = true;
            }
            if (SomethingRight() && !rightHit.collider.CompareTag("Pared"))
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
            if (enContactoConLaPared && !onFloor && !IsOnSomething())
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
            if (canJump)
            {
                if (enContactoConLaPared && deslizandose && !saltandoDePared)
                {
                    saltoPared();
                }
                else
                {
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                }
            }
        }
    }

    private void saltoPared() {
        if (SomethingRight())
        {
            rb.AddForce(new Vector3(1 * -fuerzaSaltoParedX, fuerzaSaltoParedY, 0), ForceMode.Impulse);
        }else if (SomethingLeft())
        {
            rb.AddForce(new Vector3(1 * fuerzaSaltoParedX, fuerzaSaltoParedY, 0), ForceMode.Impulse);
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
