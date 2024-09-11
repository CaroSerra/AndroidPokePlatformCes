using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Vector3 direccionMov;
    Rigidbody2D rb;

    public bool appearing = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Se ejecuta cada 0.02s de forma fija (Fisicas)
    private void FixedUpdate()
    {
        if (direccionMov.x < 0) {
            direccionMov.x = -1.8f;
        }
        else {
            direccionMov.x = 1.2f;
        }
        if (appearing) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (IsGrounded()) {
                appearing = false;
            }
            return;
        }
        rb.velocity = direccionMov * speed;
        if(!IsGrounded()) //Si dejo de tocar el suelo
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            rb.constraints = RigidbodyConstraints2D.None;

            // es (1, 0, 0) --> (-1, 0, 0)
            direccionMov *= -1f;
        }
        else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right * 0.5f, 
            Vector2.down, 
            transform.localScale.y + 0.1f, 
            whatIsGround);

          return hit.collider != null;
    }
}
