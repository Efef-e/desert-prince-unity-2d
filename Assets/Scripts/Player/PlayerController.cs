using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementDirection;
    public float speed;
    public float JumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0;

    private bool isFacingRight = true;
    private bool isGrounded;
    Rigidbody2D rb;
    Animator anim;

    public GameObject groundCheck;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();

        if (Time.time > nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection > 0)
            Flip();
        {

        }
    }
    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }
        }
    }

    public void Attack()
    {
        float numb = Random.Range(0, 2);

        if (numb == 0)
        {
            anim.SetTrigger("Attack1");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
