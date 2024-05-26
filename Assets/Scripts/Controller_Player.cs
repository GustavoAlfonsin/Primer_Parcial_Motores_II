using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public float jumpForce = 10;

    public float speed = 5;

    public int playerNumber;

    public Rigidbody rb;

    private BoxCollider col;

    public LayerMask floor; // la capa donde dectecta el Ray

    internal RaycastHit leftHit, rightHit, downHit;

    public float distanceRay, downDistanceRay;

    internal bool canMoveLeft, canMoveRight, canJump;
    internal bool onFloor;

    private Vector3 groundPosition;
    private Vector3 lastGroundPosition;

    private string groundName;
    private string lastGroundName;



    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        // Freza la posición del personaje cuando este no este activo
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    public virtual void FixedUpdate()
    {
        // Solo se puede mover si es el jugador activo
        if (GameManager.actualPlayer == playerNumber)
        {
            Movement();
        }
    }

    public virtual void Update()
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

            if (onFloor)
            {
                if (IsOnSomething())
                {
                    GameObject groundedIn = downHit.collider.gameObject;
                    groundName = groundedIn.name;
                    groundPosition = groundedIn.transform.position;
                    if (groundPosition.x != lastGroundPosition.x && groundName == lastGroundName)
                    {
                        transform.position += groundPosition - lastGroundPosition;
                    }
                    lastGroundName = groundName;
                    lastGroundPosition = groundPosition;
                }
            }
            else if (!onFloor)
            {
                lastGroundName = null;
                lastGroundPosition = Vector3.zero;
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




    public virtual bool IsOnSomething()
    {
        return Physics.BoxCast(transform.position, new Vector3(transform.localScale.x * 0.9f, transform.localScale.y / 3, transform.localScale.z * 0.9f), Vector3.down, out downHit, Quaternion.identity, downDistanceRay);
    }

    public virtual bool SomethingRight()
    {
        Ray landingRay = new Ray(new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2.2f), transform.position.z), Vector3.right);
        Debug.DrawRay(landingRay.origin, landingRay.direction, Color.green);
        return Physics.Raycast(landingRay, out rightHit, transform.localScale.x / 1.8f) && !rightHit.collider.CompareTag("ZonaRoja");
    }

    public virtual bool SomethingLeft()
    {
        Ray landingRay = new Ray(new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2.2f), transform.position.z), Vector3.left);
        Debug.DrawRay(landingRay.origin, landingRay.direction, Color.green);
        return Physics.Raycast(landingRay, out leftHit, transform.localScale.x / 1.8f) && !rightHit.collider.CompareTag("ZonaRoja");
    }

    internal virtual void Movement()
    {
        if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
            rb.velocity = new Vector3(1 * -speed, rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            rb.velocity = new Vector3(1 * speed, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        //if (!canMoveLeft)
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //if (!canMoveRight)
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public virtual void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Se apreto W");
            if (canJump)
            {
                Debug.Log("Salto");
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
            GameManager.gameOver = true;
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }

    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}
