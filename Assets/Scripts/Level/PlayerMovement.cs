using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumps = 2;
    Animator animator;

    float h;
    float w;
    float jumpsLeft = 2;
    public Joystick joystick;
    [SerializeField] public Button b;

    public int score = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

     
    #if UNITY_ANDROID
        b.onClick.AddListener(() =>
        {
            if(isGrounded()) {
                jumpsLeft = 2;
            }

            if(jumpsLeft <= 0 ) {
                return;
            }

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            jumpsLeft--;
        });
         
    #endif

    }
    // Update is called once per frame
    void Update()
    {
        h = joystick.Horizontal * moveSpeed;
        w = joystick.Vertical * moveSpeed;
        transform.position += new Vector3(h, 0, 0) * Time.deltaTime * moveSpeed;
        HandelAnimation();
        ApplyRotation();

        void HandelAnimation()
        {
            animator.SetBool("isRunning", h != 0);
            animator.SetBool("isFalling", rb.velocity.y < 0 && !isGrounded());
        }
    }

    private void ApplyRotation()
    {
        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(h, 0, 0) * moveSpeed * 3f, ForceMode2D.Force);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, whatIsGround);
        bool inGround = hit.collider != null;
        if (inGround && rb.velocity.y < 0) jumpsLeft = jumps;
        return inGround;
    }
}
